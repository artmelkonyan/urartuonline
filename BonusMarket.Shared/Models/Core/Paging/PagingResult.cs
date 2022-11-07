using System;
using System.Collections.Generic;
using System.Text;

namespace BonusMarket.Shared.Models.Core.Paging
{

    [Serializable]
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; }
        public int PageSize { get; set; } = 10;
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
