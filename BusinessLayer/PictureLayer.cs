using System;
using System.Collections.Generic;
using System.Text;
using Models;
using DataLayer;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BusinessLayer
{
    public class PictureLayer
    {
        public int? AddImage(IFormFile image, bool main, string webroot)
        {
            if (image == null)
                return null;

            string fileDirectory = Path.Combine(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
            string fileName = GetUniqueName(image.FileName);
            string uploads = Path.Combine(webroot, "uploads", "images", fileDirectory);
            var filePath = Path.Combine(uploads, fileName);
            if (!Directory.Exists(uploads))
            { 

                Directory.CreateDirectory(uploads);
            }
            var stream = new FileStream(filePath, FileMode.Create);
            image.CopyTo(stream);

            PictureEntity picEntity = new PictureEntity();

            picEntity.CreationDate = DateTime.Now;
            picEntity.SeoName = image.FileName;
            picEntity.RealPath = fileDirectory;
            picEntity.FullPath = Path.Combine(fileDirectory, fileName);
            picEntity.RealName = fileName;
            picEntity.Main = main;
            PictureDbProxy picDb = new PictureDbProxy();
            int? id = picDb.Insert(picEntity);
            return id;
        }
        public PictureEntityViewModel GetPictureById(int id)
        {
            PictureDbProxy picDb = new PictureDbProxy();

            return picDb.GetPictureById(id);
        }
        public bool RemoveImageFromProduct(int id)
        {
            PictureDbProxy db = new PictureDbProxy();

            bool status = db.RemoveProductPicture(id);

            return status;
        }
        public bool RemoveImage(int id)
        {
            PictureDbProxy db = new PictureDbProxy();
            bool status = db.RemoveImage(id);
            
            return status;
        }

        private string GetUniqueName(string fileName)
        {
            return string.Format(@"{0}_{1}", Guid.NewGuid(), fileName);
        }
    }
}
