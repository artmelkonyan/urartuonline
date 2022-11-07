using System.Collections.Generic;
using System.Linq;
using EFCore.BulkExtensions;
using Models;

namespace DataLayer
{
    public class ProductToPictureRepository
    {
        private readonly PictureMssqlContext _context;

        public ProductToPictureRepository(PictureMssqlContext dbContext)
        {
            _context = dbContext;
        }

        public List<ProductToPictureDbModel> GetList()
        {
            return _context.Product_To_Picture.ToList();
        }
        
        
        public List<ProductToPictureDbModel> GetListBy(int productId)
        {
            return _context.Product_To_Picture.Where(r => r.ProductId == productId).ToList();
        }

        public ProductToPictureDbModel Add(ProductToPictureDbModel list)
        {
            _context.Product_To_Picture.Add(list);
            _context.SaveChanges();
            // this.Save();
            // _context.BulkRead(list);
            return list;
        }
        public List<ProductToPictureDbModel> Add(List<ProductToPictureDbModel> list)
        {
            _context.BulkInsert(list, config =>
            {
                // config.TrackingEntities = true;
            });
            // this.Save();
            // _context.BulkRead(list);
            return list;
        }

        public void Update(List<ProductToPictureDbModel> list)
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