using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Repository.Shared;

namespace BonusMarket.Shared.Repository.LayoutItem
{
    public class LayoutItemRepository
    {
        private readonly Context _context;

        public LayoutItemRepository(Context context)
        {
            _context = context;
        }
        
        public Models.Core.LayoutItem Get(int id)
        {
            return _context.LayoutItems.Where(r => (r.ID == id && r.Status)).Select(r => r)
                .Include(r=> r.LayoutItemTranslations)
                .FirstOrDefault();
        }

        public PagedResult<Models.Core.LayoutItem> GetList(LayoutItemFilter filter)
        {
            var Items = _context.LayoutItems.Where(r => (r.Status)
                                                  && (r.IsActive)
                )
                .Include(r=> r.LayoutItemTranslations)
                .AsQueryable().GetPaged(filter.CurrentPage, filter.PageSize);

            return Items;
        }

        public Models.Core.LayoutItem Add(Models.Core.LayoutItem model)
        {
            model.CreationDate = DateTime.Now;
            model.Status = true;
            _context.LayoutItems.Add(model);
            Save();
            this.updateTranslations(model);
            Save();
            return model;
        }

        public void updateTranslations(Models.Core.LayoutItem model)
        {
            foreach (var elem in model.LayoutItemTranslations)
            {
                if (elem.ID == 0)
                {
                    elem.LayoutItem = model;
                    _context.LayoutItemTranslations.Add(elem);
                }
                else
                {
                    var current = _context.LayoutItemTranslations.Where( r => r.ID == elem.ID).FirstOrDefault();

                    if (current != null)
                    {
                        current.MainTitle = elem.MainTitle;
                        current.Address = elem.Address;
                        current.FooterName = elem.FooterName;
                        current.LogoImage = elem.LogoImage;
                        current.LogoShortImage = elem.LogoShortImage;
                        _context.LayoutItemTranslations.Update(current);
                    }
                }
                _context.SaveChanges();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Models.Core.LayoutItem model)
        {

            var item = _context.LayoutItems.Where(
                r => r.Status
                     && r.ID == model.ID && r.Status)
                .Include(r=> r.LayoutItemTranslations)
                .FirstOrDefault();
            item.IsActive = model.IsActive;
            item.DomainName = model.DomainName ?? item.DomainName;
            item.Twitter = model.Twitter ?? item.Twitter;
            item.BookShopUrl = model.BookShopUrl ?? item.BookShopUrl;
            item.Instagram = model.Instagram ?? item.Instagram;
            item.Facebook = model.Facebook ?? item.Facebook;
            item.CategoryImage = model.CategoryImage ?? item.CategoryImage;
            
            _context.LayoutItems.Update(item);
            _context.SaveChanges();
            
            this.updateTranslations(model);
            Save();
            
            return true;
        }


        public void Delete(int id)
        {
            var item = _context.LayoutItems.Where(
                r => r.Status
                     && r.ID == id).Include(r=> r.LayoutItemTranslations).FirstOrDefault();
            item.Status = false;
            _context.LayoutItems.Update(item);
            _context.SaveChanges();
        }
    }
}