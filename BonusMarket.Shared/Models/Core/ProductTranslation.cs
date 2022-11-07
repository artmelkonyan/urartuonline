using System;
using Newtonsoft.Json;

namespace BonusMarket.Shared.Models.Core
{
    public class ProductTranslation
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Status { get; set; }
        public string SeoName { get; set; }
        
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}