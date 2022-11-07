using System.Collections.Generic;
using System.Linq;
using EFCore.BulkExtensions;
using Models;

namespace DataLayer
{
    public class ProductTranslationRepository
    {
        private readonly ProductMssqlContext _context;

        public ProductTranslationRepository(ProductMssqlContext dbContext)
        {
            _context = dbContext;
        }

        public List<ProductTranslationDbModel> GetList()
        {
            return _context.ProductTranslation.ToList();
        }

        public List<ProductTranslationDbModel> Add(List<ProductTranslationDbModel> list)
        {
            _context.BulkInsert(list, config =>
            {
                // config.TrackingEntities = true;
            });
            // this.Save();
            // _context.BulkRead(list);
            return list;
        }

        public void Update(List<ProductTranslationDbModel> list)
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