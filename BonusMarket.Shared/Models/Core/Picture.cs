using System;

namespace BonusMarket.Shared.Models.Core
{
    public class Picture
    {
        public int Id { get; set; }
        
        public string RealPath { get; set; } = null;
        public string RealName { get; set; } = null;
        public string SeoName { get; set; } = null;
        public bool? Main { get; set; } = null;
        public string FullPath { get; set; } = null;
        public string NewPath { get; set; } = null;
        public int? FileId { get; set; } = null;
        
        public DateTime? CreationDate { get; set; }
        public bool Status { get; set; } = true;
        
        public virtual File File { get; set; }

        public Picture()
        {
            
        }

        public Picture(File file)
        {
            this.File = file;
            this.FileId = file.ID;
            this.FullPath = file.Path;
            this.RealPath = file.Path;
            this.RealName = file.FileName;
            this.SeoName = file.FileName;
        }
    }
}