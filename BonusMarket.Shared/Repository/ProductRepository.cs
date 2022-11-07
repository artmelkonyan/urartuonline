using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Repository.Shared;

namespace BonusMarket.Shared.Repository
{
    public class ProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }
        
        public Models.Core.Product Get(int id)
        {
            return _context.Products.Where(r => (r.Id == id && r.Status)).Select(r => r)
                .Include(r=> r.ProductTranslations)
                .Include(r=> r.ProductPictures)
                .Include(r=> r.Brand)
                .ThenInclude(r=> r.BrandTranslations)
                
                .FirstOrDefault();
        }

        public IEnumerable<Models.Core.Product> GetAll()
        {

            var items = _context.Products.Where(r => r.Status)
                .Include(r => r.ProductTranslations)
                .Include(r=> r.ProductPictures)
                .Include(r=> r.Brand)
                .ThenInclude(r=> r.BrandTranslations)

                .ToList();
            return items;
        }

        public PagedResult<Models.Core.Product> GetList(ProductFilter filter)
        {
            var Items = _context.Products.Where(r => (r.Status)
                )
                .Include(r=> r.ProductPictures)
                .Include(r=> r.ProductTranslations)
                .Include(r=> r.Brand)
                .ThenInclude(r=> r.BrandTranslations)
                .AsQueryable().GetPaged(filter.CurrentPage, filter.PageSize);

            return Items;
        }

        public PagedResult<Models.Core.Product> Search(ProductFilter filter)
        {
            var query = _context.Products.Where(r => (r.Status) && r.Count > 0)
                .Include(r=> r.ProductPictures)
                .ThenInclude(a => a.Picture)
                .Include(r=> r.Brand)
                .ThenInclude(r=> r.BrandTranslations)
                .Include(r=> r.ProductTranslations).Where(r =>
              (  r.ProductTranslations.Any(r =>
                    (r.Name.Contains(filter.HyName) && !String.IsNullOrEmpty(filter.HyName)) ||
                    (r.Name.Contains(filter.EngName) && !String.IsNullOrEmpty(filter.EngName)) ||
                    (r.Name.Contains(filter.RuName) && !String.IsNullOrEmpty(filter.RuName))
                    ) )
              && (filter.BrandId != null ? r.BrandId == filter.BrandId : true)
                && 
                (r.ProductPictures.Any(a => a.Picture.Main == true && a.Picture.Status && !String.IsNullOrEmpty(a.Picture.FullPath)))
                    )
                .AsQueryable();

                if (!String.IsNullOrEmpty(filter.OrderBy))
                {
                    if (filter.OrderBy == "price")
                    {

                        query = query.OrderBy(r => r.Price);
                    }
                    else if (filter.OrderBy == "priceDesc")
                    {

                        query = query.OrderByDescending(r => r.Price);
                    }
                    else if (filter.OrderBy == "name")
                    {
                        query = query.OrderBy(r => r.ProductTranslations.Where(r => r.Language == filter.LanguageCode) != null ?  r.ProductTranslations.Where(r => r.Language == filter.LanguageCode).FirstOrDefault().Name : null);
                    }
                }
                
                return query.GetPaged(filter.CurrentPage, filter.PageSize);

            // return _context.Products
            //     .Include(r => r.ProductTranslations).Where(r =>
            //         r.ProductTranslations.Any(r => EF.Functions.FreeText(r.Name, filter.ArmSearch))).ToList();
                // .Where(x =>r EF.Functions.FreeText(x.ColumnName, "search text"));
        }

        public Models.Core.Product Add(Models.Core.Product model, IEnumerable<int> images)
        {
            model.CreationDate = DateTime.Now;
            model.Status = true;
            _context.Products.Add(model);
            Save();
            this.updateTranslations(model);
            this.UpdateImages(model, images);
            Save();
            return model;
        }

        public void updateTranslations(Models.Core.Product model)
        {
            foreach (var elem in model.ProductTranslations)
            {
                if (elem.Id == 0)
                {
                    elem.Product = model;
                    _context.ProductTranslation.Add(elem);
                }
                else
                {
                    var current = _context.ProductTranslation.Where( r => r.Id == elem.Id).FirstOrDefault();

                    if (current != null)
                    {
                        current.FullDescription = elem.FullDescription;
                        current.Name = elem.Name;
                        current.SeoName = elem.SeoName;
                        current.ShortDescription = elem.ShortDescription;
                        current.FullDescription = elem.FullDescription;
                        _context.ProductTranslation.Update(current);
                    }
                }
                _context.SaveChanges();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Models.Core.Product model, IEnumerable<int> images)
        {

            var item = _context.Products.Where(
                r => r.Status
                     && r.Id == model.Id && r.Status)
                .Include(r=> r.ProductTranslations)
                .Include(r=> r.Brand)
                .ThenInclude(r=> r.BrandTranslations)
                .Include(r=> r.ProductPictures).FirstOrDefault();
            item.Count = model.Count == null ? item.Count : model.Count;
            item.Price = model.Price == null ? item.Price : model.Price;
            item.Published = model.Published == null ? item.Published : model.Published;
            item.Sku = model.Sku == null ? item.Sku : model.Sku;
            item.BrandId = model.BrandId == null ? item.BrandId : model.BrandId;
            item.OldPrice = model.Price == null ? item.OldPrice : model.Price;
            item.ShowOnHomePage = model.ShowOnHomePage == null ? item.ShowOnHomePage : model.ShowOnHomePage;
            
            _context.Products.Update(item);
            _context.SaveChanges();
            
            this.updateTranslations(model);
            this.UpdateImages(item, images);
            Save();
            
            return true;
        }

        public void UpdateImages(Models.Core.Product item, IEnumerable<int> list)
        {
            foreach (var elem in item.ProductPictures)
            {
                _context.Product_To_Picture.Remove(elem);
            }
            
            foreach (var elem in list)
            {
                _context.Product_To_Picture.Add(new ProductPicture()
                {
                    ProductId = item.Id,
                    PictureId = elem // TODO image id update part
                });
            }
        }

        public void Delete(int id)
        {
            var item = _context.Products.Where(
                r => r.Status
                     && r.Id == id).Include(r=> r.ProductTranslations).FirstOrDefault();
            item.Status = false;
            _context.Products.Update(item);
            _context.SaveChanges();
        }
    }
}