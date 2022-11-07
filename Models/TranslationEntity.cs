using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Serializable]
    public class TranslationEntity : BaseEntity
    {
        public string Language { get; set; }
    }
    public class TranslationEntityViewModel : BaseEntityViewModel
    {
        public string Language { get; set; }
    }
    
}
