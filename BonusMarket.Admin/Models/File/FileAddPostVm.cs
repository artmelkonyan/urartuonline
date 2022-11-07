using System.Collections.Generic;

namespace BonusMarket.Admin.Models.File
{
    public class FileAddPostVm : FileBaseVm
    {
        public IEnumerable<int> NewBooks { get; set; } = new List<int>();
        public FileAddPostVm()
        {
            
        }
        public FileAddPostVm(Shared.Models.Core.File item)
        {
            this.File = item;
        }
        public Shared.Models.Core.File GetDbModel()
        {
            return new Shared.Models.Core.File()
            {
                Path = this.File.Path,
            };
        }
    }
}