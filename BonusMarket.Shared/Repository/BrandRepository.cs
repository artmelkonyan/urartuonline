using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Repository.Shared;

namespace BonusMarket.Shared.Repository.Brand
{
    public class BrandRepository
    {
        private readonly Context _context;

        public BrandRepository(Context context)
        {
            _context = context;
        }

        public Models.Core.Brand Get(int id)
        {
            return _context.Brand.Where(r => (r.Id == id && r.Status)).Select(r => r)
                .Include(r => r.BrandTranslations)
                .Include(r => r.Picture)
                .ThenInclude(a => a.File)
                .FirstOrDefault();
        }

        public PagedResult<Models.Core.Brand> GetList(BrandFilter filter)
        {
            var Items = _context.Brand.Where(r => (r.Status)
                )
                .Include(r => r.BrandTranslations)
                .Include(r => r.Picture)
                .ThenInclude(a => a.File)
                .AsQueryable().GetPaged(filter.CurrentPage, filter.PageSize);

            return Items;
        }public PagedResult<Models.Core.Brand> Search(BrandFilter filter)
        {
            var query = _context.Brand.Where(r => (r.Status))
                .Include(r=> r.Picture)
                .Include(r=> r.BrandTranslations).Where(r =>
              (  r.BrandTranslations.Any(r =>
                    (r.Name.Contains(filter.HyName) && !String.IsNullOrEmpty(filter.HyName)) ||
                    (r.Name.Contains(filter.EngName) && !String.IsNullOrEmpty(filter.EngName)) ||
                    (r.Name.Contains(filter.RuName) && !String.IsNullOrEmpty(filter.RuName))
                    ) ||
            r.BrandTranslations.Any(r =>
                    (r.Name.Contains(filter.HyBrand) && !String.IsNullOrEmpty(filter.HyBrand)) ||
                    (r.Name.Contains(filter.EngBrand) && !String.IsNullOrEmpty(filter.EngBrand)) ||
                    (r.Name.Contains(filter.RuBrand) && !String.IsNullOrEmpty(filter.RuBrand))
                ))
                && 
                (r.Picture.Main == true && r.Picture.Status && !String.IsNullOrEmpty(r.Picture.FullPath))
                    )
                .AsQueryable();

                if (!String.IsNullOrEmpty(filter.OrderBy))
                {
                    if (filter.OrderBy == "price")
                    {

                        // query = query.OrderBy(r => r.Price);
                    }
                    else if (filter.OrderBy == "priceDesc")
                    {

                        // query = query.OrderByDescending(r => r.Price);
                    }
                    else if (filter.OrderBy == "name")
                    {
                        // query = query.OrderBy(r => r.ProductTranslations.Where(r => r.Language == filter.LanguageCode) != null ?  r.ProductTranslations.Where(r => r.Language == filter.LanguageCode).FirstOrDefault().Name : null);
                    }
                }
                
                return query.GetPaged(filter.CurrentPage, filter.PageSize);

            // return _context.Products
            //     .Include(r => r.ProductTranslations).Where(r =>
            //         r.ProductTranslations.Any(r => EF.Functions.FreeText(r.Name, filter.ArmSearch))).ToList();
                // .Where(x =>r EF.Functions.FreeText(x.ColumnName, "search text"));
        }

        public Models.Core.Brand Add(Models.Core.Brand model)
        {
            model.Status = true;
            _context.Brand.Add(model);
            Save();
            this.updateTranslations(model);
            Save();
            return model;
        }

        public void updateTranslations(Models.Core.Brand model)
        {
            foreach (var elem in model.BrandTranslations)
            {
                if (elem.Id == 0)
                {
                    elem.Brand = model;
                    _context.BrandTranslate.Add(elem);
                }
                else
                {
                    var current = _context.BrandTranslate.Where(r => r.Id == elem.Id).FirstOrDefault();

                    if (current != null)
                    {
                        current.Name = elem.Name;
                        current.SeoName = elem.SeoName;
                        _context.BrandTranslate.Update(current);
                    }
                }

                _context.SaveChanges();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Models.Core.Brand model)
        {
            var item = _context.Brand.Where(
                    r => r.Status
                         && r.Id == model.Id && r.Status)
                .Include(r => r.BrandTranslations)
                .FirstOrDefault();

            _context.Brand.Update(item);
            _context.SaveChanges();

            this.updateTranslations(model);
            Save();

            return true;
        }


        public void Delete(int id)
        {
            var item = _context.Brand.Where(
                    r => r.Status
                         && r.Id == id).Include(r => r.BrandTranslations)
                .Include(r => r.Picture)
                .ThenInclude(a => a.File).FirstOrDefault();
            item.Status = false;
            _context.Brand.Update(item);
            _context.SaveChanges();
        }
    }
}