using Kendo.Web.Framework.KendoUi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Kendo.Web.Framework.Extensions
{
    public static class KendoExtension
    {
        public static DataSourceResult ConvertKendoView<T>(this List<T> model, DataSourceRequest comand)
        {
            var list = new PagedList<T>(model, comand.Page, comand.PageSize);
            //item already is in cache, so return it
            return new DataSourceResult()
            {
                Data = list,
                Total = model.Count
            };
        }
        public static List<T> Filter<T>(this List<T> queryable, Filter filter)
        {
            if (filter != null && filter.Logic != null && filter.Filters.Any())
            {
                // Collect a flat list of all filters
                var filters = filter.Filters;

                // Get all filter values as array (needed by the Where method of Dynamic Linq)
                var values = filters.Where(x=>!string.IsNullOrWhiteSpace(x.Value)).Select(f =>
                {
                    if (f.Value != null)
                        return f.Value.ToLower();
                    return f.Value;
                }).ToArray();

                // Create a predicate expression e.g. Field1 = @0 And Field2 > @1
                var predicate = filter.ToExpression(filters);
                if (values.Any())
                {
                    // Use the Where method of Dynamic Linq to filter the data
                    var queryables = queryable.AsQueryable<T>();
                    queryables = queryables.Where(predicate, values);
                    queryable = queryables.ToList();
                }
            }
            return queryable;
        }
        /// <summary>
        /// Sort a collection
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="queryable">Collection</param>
        /// <param name="sort">Sort parameters</param>
        /// <returns>Result</returns>
        public static List<T> Sort<T>(this List<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort != null && sort.Any())
            {
                // Create ordering expression e.g. Field1 asc, Field2 desc
                var ordering = string.Join(",", sort.Select(s => s.ToExpression()));

                // Use the OrderBy method of Dynamic Linq to sort the data
                var queryables = queryable.AsQueryable<T>();
                queryables = queryables.OrderBy(ordering);
                queryable = queryables.ToList();
                return queryable;
            }

            return queryable;
        }

    }

}
