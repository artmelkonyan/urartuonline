using System.Collections.Generic;

namespace BonusMarket.Admin.Models
{
    public class DeleteItemsVm
    {
        public string ReturnUrl { get; set; }
        public List<int> Items { get; set; }

        public DeleteItemsVm()
        {
            Items = new List<int>();
        }
    }
}