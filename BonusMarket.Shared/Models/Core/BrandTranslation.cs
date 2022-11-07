using System;
using Newtonsoft.Json;

namespace BonusMarket.Shared.Models.Core
{
    public class BrandTranslation
    {
        public int? Id { get; set; }
        public int BrandId { get; set; }
        public string SeoName { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        
        [JsonIgnore]
        public virtual Brand Brand { get; set; }
    }
}