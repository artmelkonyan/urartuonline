using System.Collections.Generic;
using System.Linq;
using EFCore.BulkExtensions;
using Models;

namespace DataLayer
{
    public class PictureRepository
    {
        private readonly PictureMssqlContext _context;
    
        public PictureRepository(PictureMssqlContext dbContext)
        {
            _context = dbContext;
        }

        public List<PictureDbModel> GetList()
        {
            return _context.Pictures.ToList();
        }

        public List<PictureDbModel> Add(List<PictureDbModel> list)
        {
            _context.BulkInsert(list, config =>
            {
                // config.TrackingEntities = true;
            });
            // this.Save();
            // _context.BulkRead(list);
            return list;
        }
        public PictureDbModel Add(PictureDbModel list)
        {
            _context.Pictures.Add(list);
            _context.SaveChanges();
            // this.Save();
            // _context.BulkRead(list);
            return list;
        }

        public void Update(List<PictureDbModel> list)
        {
            _context.BulkUpdate(list, config =>
            {

            });
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}