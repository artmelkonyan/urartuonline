﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kendo.Web.Framework.KendoUi
{
    public class Filter
    {
        public Filter()
        {
            Filters = new List<Filter>();
        }
        /// <summary>
        /// Gets or sets the name of the sorted field (property). Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the filtering operator. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the filtering value. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the filtering logic. Can be set to "or" or "and". Set to <c>null</c> unless <c>Filters</c> is set.
        /// </summary>
        public string Logic { get; set; }

        /// <summary>
        /// Gets or sets the child filter expressions. Set to <c>null</c> if there are no child expressions.
        /// </summary>
        public List<Filter> Filters { get; set; }
        /// <summary>
        /// Converts the filter expression to a predicate suitable for Dynamic Linq e.g. "Field1 = @1 and Field2.Contains(@2)"
        /// </summary>
        /// <param name="filters">A list of flattened filters.</param>
        /// 
        /// <summary>
        /// Mapping of Kendo DataSource filtering operators to Dynamic Linq
        /// </summary>
        private static readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "DoesNotContain"}
        };
        private void Collect(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                foreach (var filter in Filters)
                {
                    filters.Add(filter);

                    filter.Collect(filters);
                }
            }
            else
            {
                filters.Add(this);
            }
        }

        public string ToExpression(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                return  string.Join(" " + Logic + " ", Filters.Select(filter => filter.ToExpression(filters)).ToArray()) ;
            }

            var index = filters.IndexOf(this);

            var comparison = operators[Operator];

            //original code below (case sensitive) commented
            //if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Contains")
            //{
            //    return $"{Field}.{comparison}(@{index})";
            //}

            //we ignore case
            if (comparison == "Contains")
            {
                return $"{Field}=@{index}, {Value} >= 0";
            }
            if (comparison == "DoesNotContain")
            {
                return $"{Field}@{index}, {Value} < 0";
            }
            if (comparison == "=" && Value is string)
            {
                //string only
                comparison = "Equals";
                //numeric values use standard "=" char
            }
            if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Equals")
            {
                return $"{Field}.ToString().ToLower().Contains(@{index})";
            } 
            return $"{Field} {comparison} @{index}";
        }
        /// <summary>
        /// Get a flattened list of all child filter expressions.
        /// </summary>
        public IList<Filter> All()
        {
            var filters = new List<Filter>();

            Collect(filters);

            return filters;
        }

    }
}
