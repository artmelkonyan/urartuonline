using Newtonsoft.Json;

namespace BonusMarket.Shared.Models.Core
{
    public class ProductPicture
    {
        public int? Id { get; set; } = null;
        public int? ProductId { get; set; } = null;
        public int? PictureId { get; set; } = null;
        
        public virtual Picture Picture { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}