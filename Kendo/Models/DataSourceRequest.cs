using System;
using System.Collections.Generic;
using System.Text;

namespace Kendo.Web.Framework.KendoUi
{
    public class DataSourceRequest
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public DataSourceRequest()
        {
            this.Page = 1;
            this.PageSize = 10;
            Filter = new Filter();
            Sort = new List<Sort>();
        }
        /// <summary>
        /// Page number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        public Filter Filter { get; set; }
        public List<Sort> Sort { get; set; }
    }
}
