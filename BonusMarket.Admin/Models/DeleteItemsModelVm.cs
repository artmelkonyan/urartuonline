using System.Collections.Generic;

namespace BonusMarket.Admin.Models
{
    public class DeleteItemsModalVm
    {
        public string ReturnUrl { get; set; }
        public string Url { get; set; }
        public IEnumerable<int> Ids { get; set; }

        public DeleteItemsModalVm(string url, IEnumerable<int> ids)
        {
            Url = url;
            Ids = ids;
        }

        public DeleteItemsModalVm()
        {
            
        }
    }
}