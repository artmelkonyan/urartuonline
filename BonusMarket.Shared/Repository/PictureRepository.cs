using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BonusMarket.Shared.DbProvider;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Repository.Shared;

namespace BonusMarket.Shared.Repository.Picture
{
    public class PictureRepository
    {
        private readonly Context _context;

        public PictureRepository(Context context)
        {
            _context = context;
        }
        
        public Models.Core.Picture Get(int id)
        {
            return _context.Pictures.Where(r => (r.Id == id && r.Status)).Select(r => r)
                .FirstOrDefault();
        }

        public PagedResult<Models.Core.Picture> GetList(PictureFilter filter)
        {
            var Items = _context.Pictures.Where(r => (r.Status)
                )
                .AsQueryable().GetPaged(filter.CurrentPage, filter.PageSize);

            return Items;
        }

        public Models.Core.Picture Add(Models.Core.Picture model)
        {
            model.CreationDate = DateTime.Now;
            model.Status = true;
            _context.Pictures.Add(model);
            Save();
            return model;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Models.Core.Picture model)
        {

            var item = _context.Pictures.Where(
                r => r.Status
                     && r.Id == model.Id && r.Status)
                .FirstOrDefault();
            item.FullPath = model.FullPath ?? item.FullPath;
            item.RealName = model.RealName ?? item.RealName;
            item.RealPath = model.RealPath ?? item.RealPath;
            item.SeoName = model.SeoName ?? item.SeoName;
            _context.Pictures.Update(item);
            _context.SaveChanges();
            
            Save();
            
            return true;
        }


        public void Delete(int id)
        {
            var item = _context.Pictures.Where(
                r => r.Status
                     && r.Id == id).FirstOrDefault();
            item.Status = false;
            _context.Pictures.Update(item);
            _context.SaveChanges();
        }
    }
}