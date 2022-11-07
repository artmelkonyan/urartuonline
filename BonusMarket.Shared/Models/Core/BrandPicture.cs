using Newtonsoft.Json;

namespace BonusMarket.Shared.Models.Core
{
    public class BrandPicture
    {
        public int? Id { get; set; } = null;
        public int? BrandId { get; set; } = null;
        public int? PictureId { get; set; } = null;
        
        public virtual Picture Picture { get; set; }
        [JsonIgnore]
        public virtual Brand Brand { get; set; }
    }
}