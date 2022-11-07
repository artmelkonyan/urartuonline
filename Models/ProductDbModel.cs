using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models
{
    public class ProductDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int Count { get; set; }
        public string Sku { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool Published { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Status { get; set; }
        public int? BrandId { get; set; }
        
        [NotMapped]
        public ProductTranslationDbModel Translation { get; set; }
    }
}