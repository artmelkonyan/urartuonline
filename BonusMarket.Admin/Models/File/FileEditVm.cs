using System.Collections.Generic;

namespace BonusMarket.Admin.Models.File
{
    public class FileEditVm : FileBaseVm
    {
        public IEnumerable<int> NewBooks { get; set; } = new List<int>();
        public int Id { get; set; }

        public FileEditVm()
        {
            
        }

        public FileEditVm(Shared.Models.Core.File item)
        {
            this.File = item;
        }
        public Shared.Models.Core.File GetDbModel()
        {
            return new Shared.Models.Core.File()
            {
                ID = this.File.ID,
                Path = this.File.Path,
            };
        }
    }
}