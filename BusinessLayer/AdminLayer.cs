using System;
using System.Xml.Linq;
using DataLayer;
using System.Collections.Generic;
using Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Globalization;

namespace BusinessLayer
{
    public class XmlAddResult
    {
        public int FromXmlCount;
        public int UpdatedCount;
        public int InsertedCount;
        public int RemovedCount;
    }
    public class AdminLayer
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductTranslationRepository _productTranslationRepository;
        private readonly PictureRepository _pictureRepository;
        private readonly ProductToPictureRepository _productToPictureRepository;
        public AdminLayer(ProductRepository productRepository, 
            ProductTranslationRepository _productTranslationRepository,
            PictureRepository pictureRepository,
            ProductToPictureRepository productToPictureRepository)
        {
            _productRepository = productRepository;
            this._productTranslationRepository = _productTranslationRepository;
            this._pictureRepository = pictureRepository;
            this._productToPictureRepository = productToPictureRepository;
        }
        public AdminLayer()
        {
        }

        public async Task<XmlAddResult> AddProductsFromXmlV2(IFormFile file)
        {
            XmlAddResult result = new XmlAddResult();


            // read xml
            List<ProductDbModel> xmlProducts = await GetXmlProducts(file);
            Dictionary<String, ProductDbModel> xmlProductsMap = GetProductMapBySku(xmlProducts);

            // old products
            List<ProductDbModel> oldProducts = _productRepository.GetList();
            Dictionary<String, ProductDbModel> oldProductsMap = GetProductMapBySku(oldProducts);
            List<ProductTranslationDbModel> OldProductTranslation = _productTranslationRepository.GetList();

            List<ProductDbModel> toInsert = new List<ProductDbModel>();
            Dictionary<String, List<ProductTranslationDbModel>> toInsertTranslation = new Dictionary<string, List<ProductTranslationDbModel>>();
            List<PictureDbModel> toInsertPictures = new List<PictureDbModel>();
            List<ProductToPictureDbModel> toInsertProductToPictures = new List<ProductToPictureDbModel>();

            List<ProductDbModel> toUpdate = new List<ProductDbModel>();

            // add new or update
            foreach (var product in xmlProducts)
            {
                if (oldProductsMap.ContainsKey(product.Sku))
                {
                    var temp = oldProductsMap[product.Sku];
                    temp.OldPrice = product.OldPrice;
                    temp.Price = product.Price;
                    temp.Count = product.Count;
                    if (temp.Count > 0)
                        temp.Published = true;
                    
                    if (!toUpdate.Any(r => r.Sku == temp.Sku))
                        toUpdate.Add(temp);

                    var oldTrs = OldProductTranslation.FindAll(x => x.ProductId == temp.Id);
                    if (product.Translation != null && temp.Id.HasValue && (oldTrs == null || !oldTrs.Any()))
                    {
                        var list = new List<ProductTranslationDbModel>();
                        var tr = product.Translation;
                        list.Add(new ProductTranslationDbModel()
                        {
                            CreationDate = DateTime.Now,
                            Name = tr.Name,
                            SeoName = tr.Name,
                            Language = "hy",
                            ProductId = temp.Id.Value
                        });
                        list.Add(new ProductTranslationDbModel()
                        {
                            CreationDate = DateTime.Now,
                            Name = tr.Name,
                            SeoName = tr.Name,
                            Language = "en",
                            ProductId = temp.Id.Value
                        }); list.Add(new ProductTranslationDbModel()
                        {
                            CreationDate = DateTime.Now,
                            Name = tr.Name,
                            SeoName = tr.Name,
                            Language = "ru",
                            ProductId = temp.Id.Value
                        });
                        toInsertTranslation.Add(product.Sku, list);
                    }
                }
                else
                {
                    product.CreationDate = DateTime.Now;
                    product.Status = true;
                    if (product.Count > 0)
                        product.Published = true;
                    toInsert.Add(product);
                    if (product.Translation != null)
                    {
                        var list = new List<ProductTranslationDbModel>();
                        var tr = product.Translation;
                        list.Add(new ProductTranslationDbModel()
                        {
                            CreationDate = DateTime.Now,
                            Name = tr.Name,
                            SeoName = tr.Name,
                            Language = "hy"
                        });
                        list.Add(new ProductTranslationDbModel()
                        {
                            CreationDate = DateTime.Now,
                            Name = tr.Name,
                            SeoName = tr.Name,
                            Language = "en",
                        }); list.Add(new ProductTranslationDbModel()
                        {
                            CreationDate = DateTime.Now,
                            Name = tr.Name,
                            SeoName = tr.Name,
                            Language = "ru",
                        });
                        toInsertTranslation.Add(product.Sku, list);
                    }

                }
            }

            // remove old
            foreach (var product in oldProducts)
            {
                if (!xmlProductsMap.ContainsKey(product.Sku))
                {
                    product.Count = 0;
                    if (!toUpdate.Any(r => r.Sku == product.Sku))
                        toUpdate.Add(product);
                }
            }


            // return await Task.Run(() => result);
            // add new
            var insertedList = _productRepository.Add(toInsert);

            // updates
            _productRepository.Update(toUpdate);

            _productRepository.Save();
            
            
            // var allProductsToPictures = _productToPictureRepository.GetList();
            // // var allPictures = _pictureRepository.GetList();
            // Dictionary<int, List<ProductToPictureDbModel>> allProductsToPicturesDictionary =
            //     new Dictionary<int, List<ProductToPictureDbModel>>(
            //     );
            // foreach (var item in allProductsToPictures)
            // {
            //     var key = item.ProductId.Value;
            //     if (!allProductsToPicturesDictionary.ContainsKey(key))
            //         allProductsToPicturesDictionary.Add(key, new List<ProductToPictureDbModel>());
            //     
            //     allProductsToPicturesDictionary[key].Add(item);
            // }
            
            List<ProductDbModel> oldProductsUpdated = _productRepository.GetList();
            List<ProductTranslationDbModel> toInsertTranslationLatest = new List<ProductTranslationDbModel>();
            foreach (var product in oldProductsUpdated)
            {

                // if (!allProductsToPicturesDictionary.ContainsKey(product.Id.Value))
                // {
                //     var insertedPicture = _pictureRepository.Add(new PictureDbModel()
                //     {
                //         CreationDate = DateTime.Now,
                //         FullPath = "",
                //         Main = true,
                //         RealName = "",
                //         RealPath = "",
                //         SeoName = "",
                //         Status = true,
                //     });
                //     var insertedProductPicture = _productToPictureRepository.Add(new ProductToPictureDbModel()
                //     {
                //         PictureId = insertedPicture.Id,
                //         ProductId = product.Id,
                //     });
                // }
                
                if (!toInsertTranslation.ContainsKey(product.Sku))
                    continue;
                
                var translation = toInsertTranslation[product.Sku];
                foreach (var item in translation)
                {
                    item.ProductId = product.Id.Value;
                    toInsertTranslationLatest.Add(item);
                }
                
            }
            _productTranslationRepository.Add(toInsertTranslationLatest);
            _productTranslationRepository.Save();
            
            /*
            List<PictureEntity> currentProductPictureList = picDb.GetProductPictures(entity.Id);
            foreach (XElement prodImage in productImages)
            {
                PictureEntity picEntity = new PictureEntity();

                picEntity.CreationDate = DateTime.Now;
                picEntity.SeoName = prodImage.Element("SeoName").Value;
                picEntity.RealPath = prodImage.Element("RealPath").Value;
                picEntity.FullPath = prodImage.Element("FullPath").Value;
                picEntity.RealName = prodImage.Element("RealName").Value;

                int? curPicId = picDb.Insert(picEntity);

                if (curPicId == null)
                {
                    status = false;
                }
                else
                {
                    ProductPictureMapping tempMappingEntity = new ProductPictureMapping();
                    tempMappingEntity.PictureId = curPicId.Value;
                    tempMappingEntity.ProductId = id.Value;

                    int? mappingId = picDb.InsertProductMapping(tempMappingEntity);
                    if (mappingId == null)
                        status = false;
                }
            }
            
            // foreach (var item in insertedList)
            // {
            //     if (item.Id != null)
            //     {
            //         int a = 1;
            //     }
            // }
            //
            */
            return await Task.Run(() => result);
        }

        public Dictionary<string, ProductDbModel> GetProductMapBySku(List<ProductDbModel> list)
        {
            Dictionary<String, ProductDbModel> result = new Dictionary<string, ProductDbModel>();

            foreach (var item in list)
            {
                if (!result.ContainsKey(item.Sku))
                {
                    result.Add(item.Sku, item);
                }
            }

            return result;
        }

        public async Task<List<ProductDbModel>> GetXmlProducts(IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            XDocument xml = null;
            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Dispose();

                    xml = XDocument.Load(filePath);
                }
            }
            var productList = xml.Element("Products").Elements("Product");
            List<ProductDbModel> xmlProductList = new List<ProductDbModel>();
            List<string> xmlSkus = new List<string>();
            //List<string> skusList = new List<string>();
            foreach (XElement item in productList)
            {
                ProductDbModel product = new ProductDbModel();
                product.Price = Decimal.Parse(item.Element("Price").Value, CultureInfo.InvariantCulture);
                product.OldPrice = (item.Element("OldPrice").Value == "" ? 0 : Decimal.Parse(item.Element("OldPrice").Value, CultureInfo.InvariantCulture));
                product.Sku = item.Element("Sku").Value;
                product.Count = Int32.Parse(item.Element("Count").Value, CultureInfo.InvariantCulture);
                product.Translation = new ProductTranslationDbModel()
                {
                    Name = item.Element("Name").Value
                };

                xmlSkus.Add("'" + item.Element("Sku").Value + "'");
                xmlProductList.Add(product);
                //skusList.Add(item.Element("Sku").Value);
            }

            return await Task.Run(() => xmlProductList);
        }
        public async Task<bool> AddProductsFromXml(IFormFile file)
        {
            AdminDbProxy db = new AdminDbProxy();
            var filePath = Path.GetTempFileName();

            XDocument xml = null;
            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Dispose();

                    xml = XDocument.Load(filePath);
                }
            }
            var productList = xml.Element("Products").Elements("Product");
            List<ProductEntity> xmlProductList = new List<ProductEntity>();
            List<string> xmlSkus = new List<string>();
            //List<string> skusList = new List<string>();
            foreach (XElement item in productList)
            {
                ProductEntity product = new ProductEntity();
                product.Price = Decimal.Parse(item.Element("Price").Value);
                product.OldPrice = (item.Element("OldPrice").Value == "" ? 0 : Decimal.Parse(item.Element("OldPrice").Value));
                product.Sku = item.Element("Sku").Value;
                product.Count = Int32.Parse(item.Element("Count").Value);
                product.Translation = new ProductTranslationEntity
                {
                    NameTranslation = item.Element("Name").Value
                };

                xmlSkus.Add("'" + item.Element("Sku").Value + "'");
                xmlProductList.Add(product);
                //skusList.Add(item.Element("Sku").Value);
            }

            string formatedList = string.Join(",", xmlSkus);

            Task.Factory.StartNew(() => AddXmlProductsList(xmlProductList, formatedList));
            #region
            //List<ProductEntity> CurrentProducts = db.GetProductsBySkuList(formatedList);

            //List<ProductEntity> CurrentProductsAll = db.GetProductsByExcludedSkuList(formatedList);

            //List<ProductEntity> CurrentProductsNotInXml = new List<ProductEntity>();

            //var skus = CurrentProductsAll.Where(r => !xmlProductList.Any(a => a.Sku == r.Sku)).ToList();

            ////CurrentProductsNotInXml = CurrentProductsNotInXml.Where(r => r.Sku == "06445550010079").ToList();

            //List<string> xmlSkusNot = new List<string>();
            //foreach (var item in skus)
            //{

            //    xmlSkusNot.Add("'" + item.Sku + "'");
            //}
            //string formatedListNot = string.Join(",", xmlSkusNot);

            //db.UpdateCountBySkuList(formatedListNot);

            //ProductDbProxy productDb = new ProductDbProxy();

            //List<Task> TaskList = new List<Task>();

            //int k = 0;
            //foreach (ProductEntity item in xmlProductList)
            //{
            //    //bool curStatus = new ProductDbProxy().UpdateProductTranslation(item);
            //    //if (!curStatus)
            //    //    status = false;
            //    var LastTask = Task.Run(() => this.ProceedProductUpdateAsync(CurrentProducts, new ProductEntity
            //    {
            //        Price = item.Price,
            //        OldPrice = item.OldPrice,
            //        Sku = item.Sku,
            //        Count = item.Count,
            //        Translation = new ProductTranslationEntity
            //        {
            //            NameTranslation = item.Translation.NameTranslation,
            //        }
            //    }));
            //    TaskList.Add(LastTask);
            //    if (k == 30)
            //    {
            //        Task.WaitAll(TaskList.ToArray());
            //        TaskList.Clear();
            //        k = 0;
            //    }
            //}
            //Task.WaitAll(TaskList.ToArray());

            ////bool result = db.AddProductsFromXml(xmlProductList);
            #endregion
            return true;
        }
        public void UpdateProduct(ProductEntity product)
        {
            try
            {
                ProductDbProxy productDb = new ProductDbProxy();
                productDb.UpdateProduct(product);
            }
            catch (Exception)
            {
            }
        }
        private async Task AddXmlProductsList(List<ProductEntity> list, string skuList)
        {
            ProductDbProxy productDb = new ProductDbProxy();
            AdminDbProxy adp = new AdminDbProxy();
            var oldList = adp.GetAllProducts();
            int count = 0;

            foreach (var item in list)
            {
                count++;
                if (count % 100 == 0)
                {
                    Debug.WriteLine(count);
                }
                var prod = oldList.FirstOrDefault(x => x.Sku == item.Sku);
                if (prod != null && prod.Id != null && prod.OldPrice == item.OldPrice && prod.Price == item.Price && item.Count == prod.Count)
                {
                    continue;
                }
                if (prod != null && prod.Id != null)
                {
                    prod.Price = item.Price;
                    prod.OldPrice = item.OldPrice;
                    prod.Count = item.Count;

                    productDb.UpdateProduct(prod);
                    oldList.RemoveAll(x => x.Sku == prod.Sku);
                }
                else
                {
                    var id = productDb.Insert(item);
                    if (id.HasValue && item.Translation != null)
                    {
                        int nweId = id.Value;
                        productDb.InsertTranslation(new ProductTranslationEntity
                        {
                            ProductId = nweId,
                            NameTranslation = item.Translation.NameTranslation,
                            Language = "hy"
                        });
                        productDb.InsertTranslation(new ProductTranslationEntity
                        {
                            ProductId = nweId,
                            NameTranslation = item.Translation.NameTranslation,
                            Language = "en"
                        });
                        productDb.InsertTranslation(new ProductTranslationEntity
                        {
                            ProductId = nweId,
                            NameTranslation = item.Translation.NameTranslation,
                            Language = "ru"
                        });
                    }
                }
            }
            foreach (var item in oldList)
            {
                item.Count = 0;
                productDb.UpdateProduct(item);
            }
            //adp.UpdateCountBySkuList(skuList);
        }
        public int? AddProduct(ProductEntity model)
        {
            ProductDbProxy pdb = new ProductDbProxy();
            int? id = pdb.Insert(model);

            if (id.HasValue && model.TranslationList != null)
            {
                foreach (var item in model.TranslationList)
                {
                    item.ProductId = id;
                    pdb.InsertTranslation(item);
                }
            }
            return id;
        }
        private async Task<bool> ProceedProductUpdateAsync(List<ProductEntity> CurrentProducts, ProductEntity item)
        {
            bool status = true;

            ProductDbProxy productDb = new ProductDbProxy();
            ProductEntity CurItem = new ProductEntity();

            if (CurrentProducts.FindAll(r => r.Sku == item.Sku).Count > 0)
            {
                CurItem = CurrentProducts.FindAll(r => r.Sku == item.Sku)[0];
                productDb.UpdateProduct(new ProductEntity
                {
                    Sku = CurItem.Sku,
                    Price = item.Price,
                    OldPrice = item.OldPrice,
                    Count = item.Count
                });
            }
            else
            {
                int? curId = productDb.Insert(new ProductEntity
                {
                    Sku = item.Sku,
                    Price = item.Price,
                    OldPrice = item.OldPrice,
                    Count = item.Count,
                    CreationDate = DateTime.Now,
                    Published = false
                });

                if (curId != null)
                {
                    int nweId = curId.Value;
                    curId = productDb.InsertTranslation(new ProductTranslationEntity
                    {
                        ProductId = nweId,
                        NameTranslation = item.Translation.NameTranslation,
                        Language = "hy"
                    });
                    curId = productDb.InsertTranslation(new ProductTranslationEntity
                    {
                        ProductId = nweId,
                        NameTranslation = item.Translation.NameTranslation,
                        Language = "en"
                    });
                    curId = productDb.InsertTranslation(new ProductTranslationEntity
                    {
                        ProductId = nweId,
                        NameTranslation = item.Translation.NameTranslation,
                        Language = "ru"
                    });
                }
            }

            return status;
        }

        //private async Task<bool> ProceedProductCountUpdateAsync(string formatedList)
        //{
        //    bool status = true;

        //    ProductDbProxy productDb = new ProductDbProxy();
        //    ProductEntity CurItem = new ProductEntity();

        //    foreach (var old in oldList)
        //    {
        //        productDb.UpdateProduct(new ProductEntity
        //        {
        //            Sku = old.Sku,
        //            Count = 0
        //        });
        //    }

        //    return status;
        //}
        public List<ProductEntity> GetDashboardProducts(int curPage, int pageSize)
        {
            List<ProductEntity> p_list = new List<ProductEntity>();
            AdminDbProxy db = new AdminDbProxy();
            p_list = db.GetDashboardProducts(curPage, pageSize);
            return p_list;
        }
        public List<ProductEntity> GetAllProducts()
        {
            AdminDbProxy db = new AdminDbProxy();
            return db.GetAllProducts();
        }

        public List<OrderEntity> GetDashboardOrders(int catPage, int catPageSize)
        {
            AdminDbProxy db = new AdminDbProxy();
            return db.GetDashboardOrders(catPage, catPageSize);
        }
        public List<CategoryEntityDashboardViewModel> GetDashboardgategories(int catPage, int catPageSize, string searchSours)
        {
            List<CategoryEntityDashboardViewModel> c_list = new List<CategoryEntityDashboardViewModel>();
            AdminDbProxy db = new AdminDbProxy();
            if (string.IsNullOrWhiteSpace(searchSours))
                c_list = db.GetDashboardgategories(catPage, catPageSize);
            else
            {
                c_list = db.GetDashboardsCategoriesSearch(catPage, catPageSize, searchSours);
                c_list = c_list;
            }
            return c_list;
        }
        public List<CategoryEntityDashboardViewModel> GetDashboardgategories(int catPage, int catPageSize)
        {
            List<CategoryEntityDashboardViewModel> c_list = new List<CategoryEntityDashboardViewModel>();
            AdminDbProxy db = new AdminDbProxy();
            c_list = db.GetDashboardgategories(catPage, catPageSize);
            return c_list;
        }
        public void EditOrder(OrderEntityViewModel ord)
        {
            AdminDbProxy db = new AdminDbProxy();
            db.EditOrder(ord);
        }
        public ProductEntity GetProductBySku(string sku)
        {
            ProductEntity product = new ProductEntity();
            AdminDbProxy db = new AdminDbProxy();
            product = db.GetProductBySku(sku);
            return product;
        }
        public ProductEditViewModel GetProductById(int? id)
        {
            ProductEditViewModel product = new ProductEditViewModel();
            AdminDbProxy db = new AdminDbProxy();
            product = db.GetProductById(id);
            return product;
        }
        public CategoryEntityDashboardViewModel GetCategoryById(int? id)
        {
            CategoryEntityDashboardViewModel category = new CategoryEntityDashboardViewModel();
            AdminDbProxy db = new AdminDbProxy();
            category = db.GetCategoryById(id);
            return category;
        }

        public bool EditProduct(ProductEditViewModel entity, IFormFile mainImage, IFormFileCollection files, string webroot)
        {
            bool status = true;
            AdminDbProxy db = new AdminDbProxy();
            ProductDbProxy prodDb = new ProductDbProxy();
            PictureDbProxy picDb = new PictureDbProxy();
            PictureLayer picLayer = new PictureLayer();
            ProductEntity curProduct = prodDb.GetProductById(entity.Id);
            CategoryDbProxy catDb = new CategoryDbProxy();
            List<PictureEntity> currentProductPictureList = picDb.GetProductPictures(entity.Id);

            if (mainImage != null)
            {
                foreach (PictureEntity item in currentProductPictureList)
                {
                    if (item.Main.Value)
                    {
                        bool removed = picLayer.RemoveImage(item.Id.Value);
                        if (!removed)
                            status = false;
                        removed = picLayer.RemoveImageFromProduct(item.Id.Value);
                        if (!removed)
                            status = false;
                    }
                }

                int? curId = picLayer.AddImage(mainImage, true, webroot);

                if (curId != null)
                {

                    ProductPictureMapping tempMappingEntity = new ProductPictureMapping();
                    tempMappingEntity.PictureId = curId;
                    tempMappingEntity.ProductId = entity.Id;

                    int? mappingId = picDb.InsertProductMapping(tempMappingEntity);
                    if (mappingId == null)
                        status = false;
                }
                else
                {
                    status = false;
                }
            }

            if (entity.PictureList != null)
            {
                foreach (PictureEntityViewModel item in entity.PictureList)
                {
                    if (item.Status)
                    {
                        bool removed = picLayer.RemoveImage(item.Id);
                        if (!removed)
                            status = false;
                        removed = picLayer.RemoveImageFromProduct(item.Id);
                        if (!removed)
                            status = false;
                    }
                }
            }

            if (files != null)
            {

                foreach (IFormFile item in files)
                {

                    int? curId = picLayer.AddImage(item, false, webroot);

                    if (curId != null)
                    {

                        ProductPictureMapping tempMappingEntity = new ProductPictureMapping();
                        tempMappingEntity.PictureId = curId;
                        tempMappingEntity.ProductId = entity.Id;

                        int? mappingId = picDb.InsertProductMapping(tempMappingEntity);
                        if (mappingId == null)
                            status = false;
                    }
                    else
                    {
                        status = false;
                    }
                }
            }

            curProduct.Price = entity.Price;
            curProduct.Sku = entity.Sku;
            curProduct.OldPrice = entity.OldPrice;
            curProduct.Count = entity.Count;
            curProduct.ShowOnHomePage = entity.ShowOnHomePage;
            curProduct.Published = entity.Published;
            curProduct.ModificationDate = DateTime.Now;
            curProduct.BrandId = entity.BrandId;
            bool prodUpdated = prodDb.UpdateProduct(curProduct);

            if (prodUpdated)
            {
                foreach (ProductTranslationEntityViewModel item in entity.TranslationList)
                {
                    ProductTranslationEntity currentTranslation = new ProductTranslationEntity();
                    currentTranslation.Id = item.Id;
                    currentTranslation.NameTranslation = item.NameTranslation;
                    currentTranslation.ShortDescriptionTranslation = item.ShortDescriptionTranslation;
                    currentTranslation.FullDescriptionTranslation = item.FullDescriptionTranslation;
                    currentTranslation.SeoName = item.SeoName;

                    bool translationUpdated = prodDb.UpdateProductTranslation(currentTranslation);
                    if (!translationUpdated)
                    {
                        status = false;
                    }
                }
            }
            else
            {
                status = false;
            }

            if (entity.CategoryId != null && entity.CategoryId != 0)
            {
                if (!catDb.SetProductCategory(new ProductCategoryMappingEntity
                {
                    Productid = entity.Id,
                    CategoryId = entity.CategoryId
                }))
                {
                    status = false;
                }
            }

            return status;
        }
        public void EditCateGory(CategoryEntityDashboardViewModel model, IFormFile categoryImage, string webroot)
        {
            PictureDbProxy picDb = new PictureDbProxy();
            PictureLayer picLayer = new PictureLayer();
            AdminDbProxy db = new AdminDbProxy();

            if (categoryImage != null)
            {
                if (model.PictureId != 0)
                {
                    picLayer.RemoveImage(model.PictureId);

                }
                int? curId = picLayer.AddImage(categoryImage, true, webroot);

                if (curId != null)
                {
                    model.PictureId = curId.Value;
                }
            }
            db.EditCateGory(model);
            foreach (var item in model.TranslationList)
            {
                db.EditCategoryTranslation(item);
            }
        }
        public int? CreateCateGory(CategoryEntityDashboardViewModel model, IFormFile categoryImage, string webroot)
        {
            PictureDbProxy picDb = new PictureDbProxy();
            PictureLayer picLayer = new PictureLayer();
            AdminDbProxy db = new AdminDbProxy();
            CategoryDbProxy c_dbProxy = new CategoryDbProxy();
            var modelEf = new CategoryEntity()
            {
                ShowOnLayout = model.ShowOnLayout,
                Published = model.Published,
                DisplayOrder = model.DisplayOrder,
                ParentId = model.ParentId,
                ShowOnHomePage = model.ShowOnHomePage
            };
            if (categoryImage != null)
            {
                int? curId = picLayer.AddImage(categoryImage, true, webroot);
                if (curId != null)
                {
                    modelEf.PictureId = curId.Value;
                }
            }
            var id = c_dbProxy.Insert(modelEf);
            if (id != null)
            {
                foreach (var item in model.TranslationList)
                {
                    c_dbProxy.InsertTranslation(new CategoryTranslationEntity
                    {
                        CategoryId = id.Value,
                        Language = item.Language,
                        DescriptionTranslation = item.DescriptionTranslation,
                        Name = item.Name,
                        SeoName = item.SeoName
                    });
                }
            }
            return id;
        }
    }
}
