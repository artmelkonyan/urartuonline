using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class PictureDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string RealPath { get; set; }
        public string RealName { get; set; }
        public string SeoName { get; set; }
        public bool? Main { get; set; }
        public string FullPath { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? Status { get; set; }
    }
}