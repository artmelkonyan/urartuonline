using System.Collections.Generic;
using System.Linq;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;
using Models.config;

namespace DataLayer
{
    
    public class ProductRepository
    {
        private readonly ProductMssqlContext _context;

        public ProductRepository(ProductMssqlContext dbContext)
        {
            _context = dbContext;
        }

        public List<ProductDbModel> GetList()
        {
            return _context.Products.ToList();
        }

        public List<ProductDbModel> Add(List<ProductDbModel> list)
        {
            _context.BulkInsert(list, config =>
            {
                // config.TrackingEntities = true;
            });
            // this.Save();
            // _context.BulkRead(list);
            return list;
        }

        public void Update(List<ProductDbModel> list)
        {
            _context.BulkUpdate(list, config =>
            {
                config.UpdateByProperties = new List<string>(){ "Sku"};
                config.PropertiesToInclude = new List<string>(){ "Price", "OldPrice", "Count"};
            });
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}