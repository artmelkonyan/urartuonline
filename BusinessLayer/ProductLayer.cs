using System;
using System.Xml.Linq;
using DataLayer;
using System.Collections.Generic;
using Models;

namespace BusinessLayer
{
    public class ProductLayer
    {
        public int? InsertOrder(OrderEntityViewModel model, string username)
        {
            ProductDbProxy db = new ProductDbProxy();
            var result = db.InsertOrder(model, username);
            return result;
        }
        public Tuple<int, List<ProductEntity>> Search(string searchword, string searchwordFixed, int CurrentPage, int ViewCount, string lang, string Order)
        {
            ProductDbProxy db = new ProductDbProxy();
            Tuple<int, List<ProductEntity>> tuple;
            tuple = db.Search(searchword, searchwordFixed, CurrentPage, ViewCount, lang, Order);
            return tuple;
        }
        public Tuple<int, List<ProductEntity>> SearchByBrandName(string searchword, string searchwordFixed, int CurrentPage, int ViewCount, string lang, string Order)
        {
            ProductDbProxy db = new ProductDbProxy();
            Tuple<int, List<ProductEntity>> tuple;
            tuple = db.SearchByBrandName(searchword, searchwordFixed, CurrentPage, ViewCount, lang, Order);
            return tuple;
        }
        public Tuple<int, List<ProductEntity>> SearchByBrandNameAndProductName(string brandName, string searchword, string searchwordFixed, int CurrentPage, int ViewCount, string lang, string Order)
        {
            ProductDbProxy db = new ProductDbProxy();
            Tuple<int, List<ProductEntity>> tuple;
            tuple = db.SearchByBrandNameAndProductName(brandName,searchword, searchwordFixed, CurrentPage, ViewCount, lang, Order);
            return tuple;
        }

        public List<ProductEditViewModel> GetProductPrices(List<int?> idlist)
        {

            ProductDbProxy db = new ProductDbProxy();
            string result = "";
            foreach (var item in idlist)
            {
                if (item != null && item.Value > 0)
                {
                    result = result + item + ',';
                }
            }
            result = result.TrimEnd(',');
            var resp = db.GetProductPrices(result);
            return resp;
        }
        public List<ProductEntity> GetProductByIdList(string idList)
        {
            return new ProductDbProxy().GetProductByIdList(idList);
        }
        public void InsertProducts(XDocument xml)
        {
            var productList = xml.Element("Products").Elements("Product");

            List<ProductEntity> modelList = new List<ProductEntity>();

            ProductDbProxy db = new ProductDbProxy();

            foreach (XElement item in productList)
            {
                ProductEntity product = new ProductEntity();
                product.Price = Decimal.Parse(item.Element("Price").Value);
                product.OldPrice = (item.Element("OldPrice").Value == "" ? 0 : Decimal.Parse(item.Element("OldPrice").Value));
                product.Sku = item.Element("Sku").Value;
                product.Count = Int32.Parse(item.Element("Count").Value);

                modelList.Add(product);
            }

            db.Insert(modelList);
        }
        public ProductEntity GetOneProductById(int? id, string RequestLanguage)
        {
            ProductEntity oneproduct = null;
            ProductDbProxy db = new ProductDbProxy();
            oneproduct = db.GetOneProductById(id, RequestLanguage);
            PictureDbProxy dbPic = new PictureDbProxy();
            if (oneproduct != null)
            {
                oneproduct.PictureList = dbPic.GetProductPictures(id);
                if (oneproduct.PictureList != null)
                {
                    foreach (PictureEntity item in oneproduct.PictureList)
                    {
                        if (item.Main.Value)
                        {
                            oneproduct.MainImage = item;
                            break;
                        }
                    }
                }
            }
            return oneproduct;
        }
        public List<ProductEntity> GetProductList()
        {
            List<ProductEntity> products = null;
            ProductDbProxy db = new ProductDbProxy();
            products = db.GetProductList();
            return products;
        }
        public Tuple<int, List<ProductEntity>> GetProductsByBrandId(int brId, int currentPage, int pageSize, string lang,string orderBy=null)
        {
            ProductDbProxy db = new ProductDbProxy();
            return db.GetByBrandId(brId, currentPage, pageSize, lang,orderBy);
        }
        public Tuple<int, List<ProductEntity>> GetProductsByBrandListId(int[] brId, int currentPage, int pageSize, string lang, string orderBy = null)
        {
            ProductDbProxy db = new ProductDbProxy();
            return db.GetProductsByBrandListId(brId, currentPage, pageSize, lang, orderBy);
        }
        public Tuple<int, List<ProductEntity>> GetProductsByBrandListId(int[] brId,int categoryId, int currentPage, int pageSize, string lang, string orderBy = null)
        {
            ProductDbProxy db = new ProductDbProxy();
            return db.GetProductsByCategoryIdAndBrandListId(brId,categoryId, currentPage, pageSize, lang, orderBy);
        }
        public Tuple<int, List<ProductEntity>> GetProductListForUser(int CategoryId, int CurrentPage, int ViewCount, string OrderBy, string RequestLanguage)
        {
            ProductDbProxy db = new ProductDbProxy();
            Tuple<int, List<ProductEntity>> tuple;
            tuple = db.GetProductListForUser(CategoryId, CurrentPage, ViewCount, OrderBy, RequestLanguage);
            return tuple;
        }
        public Tuple<int, List<ProductEntity>> GetProductListForUser(int brandId, int CategoryId, int CurrentPage, int ViewCount, string OrderBy, string RequestLanguage)
        {
            ProductDbProxy db = new ProductDbProxy();
            Tuple<int, List<ProductEntity>> tuple;
            tuple = db.GetProductListForUser( brandId, CategoryId, CurrentPage, ViewCount, OrderBy, RequestLanguage);
            return tuple;
        }
        public List<ProductEntity> GetHomePageProducts(string lang)
        {
            List<ProductEntity> products = new List<ProductEntity>();
            ProductDbProxy db = new ProductDbProxy();
            products = db.GetHomePageProducts(lang);

            PictureDbProxy dbPic = new PictureDbProxy();
            foreach (ProductEntity oneproduct in products)
            {
                oneproduct.PictureList = dbPic.GetProductPictures(oneproduct.Id.Value);
                if (oneproduct.PictureList != null)
                {
                    foreach (PictureEntity item in oneproduct.PictureList)
                    {
                        if (item.Main.Value)
                        {
                            oneproduct.MainImage = item;
                            break;
                        }
                    }
                }

                if (oneproduct.MainImage == null)
                {
                    oneproduct.MainImage = new PictureEntity { };
                }
            }
            return products;
        }
    }
}
