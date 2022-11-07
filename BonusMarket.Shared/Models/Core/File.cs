using System;
using System.Collections.Generic;

namespace BonusMarket.Shared.Models.Core
{
    public class File
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Status { get; set; } = true;
    }
}