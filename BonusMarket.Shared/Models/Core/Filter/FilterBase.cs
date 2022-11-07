namespace BonusMarket.Shared.Models.Core.Filter
{
    public class FilterBase
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}