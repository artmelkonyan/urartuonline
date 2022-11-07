using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Models;
using DataLayer;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TempLayer
    {
        public bool SetProductDescs()
        {
            bool status = true;
            List<ProductEntity> oldList = new TempDbProxy().GetProductDescs();
            List<ProductEntity> curList = new ProductDbProxy().GetProductList();
            List<ProductTranslationEntity> trEntity = new List<ProductTranslationEntity>();

            foreach (ProductEntity oldItem in oldList)
            {
                foreach (ProductEntity curItem in curList)
                {
                    if (oldItem.Sku == curItem.Sku)
                    {
                        trEntity.Add(new ProductTranslationEntity
                        {
                            Id= curItem.Translation.Id,
                            ShortDescriptionTranslation = oldItem.Translation.ShortDescriptionTranslation,
                            FullDescriptionTranslation = oldItem.Translation.FullDescriptionTranslation
                        });
                    }
                }
            }
            List<Task> TaskList = new List<Task>();


            foreach (ProductTranslationEntity item in trEntity)
            {
                //bool curStatus = new ProductDbProxy().UpdateProductTranslation(item);
                //if (!curStatus)
                //    status = false;
                var LastTask = Task.Run(() => new ProductDbProxy().UpdateProductTranslation(new ProductTranslationEntity
                {
                    Id = item.Id,
                    ShortDescriptionTranslation = item.ShortDescriptionTranslation,
                    FullDescriptionTranslation = item.FullDescriptionTranslation
                }));
                TaskList.Add(LastTask);
            }

            Task.WaitAll(TaskList.ToArray());

            return status;
        }
        public bool MapCategoryOldIdsToRealIdsOnes(string SavePath)
        {
            bool status = true;

            var categoryList = XDocument.Load(SavePath).Element("Categories").Elements("Category");

            List<CategoryEntity> firstCategoryList = new List<CategoryEntity>();
            List<CategoryEntity> secondCategoryList = new List<CategoryEntity>();

            foreach (XElement item in categoryList)
            {
                CategoryEntity entity = new CategoryEntity
                {
                    Id = Int32.Parse(item.Element("Id").Value),
                    ParentId = Int32.Parse(item.Element("ParentId").Value),
                    OldId = Int32.Parse(item.Element("OldId").Value)
                };

                firstCategoryList.Add(entity);
                secondCategoryList.Add(entity);
            }


            List<CategoryEntity> newList = new List<CategoryEntity>();
            foreach (CategoryEntity firstItem in firstCategoryList)
            {
                foreach (CategoryEntity secondItem in secondCategoryList)
                {
                    if (firstItem.OldId == secondItem.ParentId)
                    {
                        newList.Add(new CategoryEntity
                        {
                            Id = secondItem.Id,
                            ParentId = firstItem.Id
                        });
                    }

                    if (secondItem.ParentId == 142)
                    {
                        string b = "bozi txa";
                    }
                }
            }

            foreach (CategoryEntity secondItem in newList)
            {

                if (secondItem.ParentId == 142)
                {
                    string b = "bozi txa";
                }
                status = new CategoryDbProxy().UpdateCategory(secondItem);

                if (!status)
                {
                    break;
                }
            }

            return status;
        }
        public XDocument GenerateProductPictureXDocument(XDocument xml)
        {

            var productList = xml.Element("Products").Elements("Product");

            List<ProductEntity> modelList = new List<ProductEntity>();

            TempDbProxy db = new TempDbProxy();

            string MyString = "6 May 1818";
            DateTime dataTime = DateTime.Parse(MyString);


            foreach (XElement item in productList)
            {
                ProductEntity product = new ProductEntity();
                product.Price = Decimal.Parse(item.Element("Price").Value);
                product.OldPrice = (item.Element("OldPrice").Value == "" ? 0 : Decimal.Parse(item.Element("OldPrice").Value));
                product.Sku = item.Element("Sku").Value;
                product.Count = Int32.Parse(item.Element("Count").Value);

                modelList.Add(product);

                List<ProductPictureEntity> ppList = db.SelectPictureListBySku(item.Element("Sku").Value);

                foreach (ProductPictureEntity ppItem in ppList)
                {
                    dataTime = dataTime.AddDays(1);
                    item.Add(new XElement("PicId", ppItem.PicId));
                    item.Add(new XElement("PicName", ppItem.PicName));
                    item.Add(new XElement("CreationDate", dataTime));
                }
            }
            return xml;
        }
        public XDocument GenerateCategoryPictureXDocument(XDocument xml)
        {
            List<XElement> categoryElementList = new List<XElement>();

            string MyString = "6 May 1918";
            DateTime dataTime = DateTime.Parse(MyString);

            List<CategoryEntity> categoryList = new List<CategoryEntity>();

            TempDbProxy tempDb = new TempDbProxy();

            categoryList = tempDb.GetOldCategories();

            foreach (CategoryEntity item in categoryList)
            {
                dataTime = dataTime.AddDays(1);
                XElement elem = new XElement("Category");
                elem.Add(new XElement("DisplayOrder", item.DisplayOrder));
                elem.Add(new XElement("ParentId", item.ParentId));
                elem.Add(new XElement("PictureId", item.PictureId));
                elem.Add(new XElement("ShowOnHomePage", item.ShowOnHomePage));
                elem.Add(new XElement("CreationDate", dataTime));
                elem.Add(new XElement("Name", item.Translation.Name));
                elem.Add(new XElement("PicId", item.CategoryPicture.PicId));
                elem.Add(new XElement("PicName", item.CategoryPicture.PicName));
                elem.Add(new XElement("DescriptionTranslation", item.Translation.DescriptionTranslation));

                categoryElementList.Add(elem);
            }

            xml.Add(new XElement("Categories", categoryElementList));
            return xml;
        }

        public bool SaveProductWithPictures(XDocument xml)
        {
            bool status = true;

            var productList = xml.Element("Products").Elements("Product");

            foreach (XElement item in productList)
            {

                bool hasImage = false;
                if (item.Element("CreationDate") != null && item.Element("PicId") != null)
                    hasImage = true;

                ProductEntity product = new ProductEntity();
                product.Price = Decimal.Parse(item.Element("Price").Value);
                product.OldPrice = (item.Element("OldPrice").Value == "" ? 0 : Decimal.Parse(item.Element("OldPrice").Value));
                product.Sku = item.Element("Sku").Value;
                product.Count = Int32.Parse(item.Element("Count").Value);
                product.CreationDate = DateTime.Now;

                product.Translation = new ProductTranslationEntity();
                product.Translation.NameTranslation = item.Element("Name").Value;

                if (product.Count > 0 && hasImage)
                    product.Published = true;

                ProductDbProxy db = new ProductDbProxy();

                int? id = db.Insert(product);

                if (id != null)
                {
                    product.Translation.ProductId = id.Value;
                    product.Translation.Language = "hy";
                    int? transId = db.InsertTranslation(product.Translation);
                    if (transId == null)
                        status = false;

                    ProductTranslationEntity otherLang = new ProductTranslationEntity();
                    otherLang.ProductId = id.Value;
                    otherLang.Language = "en";
                    transId = db.InsertTranslation(otherLang);
                    if (transId == null)
                        status = false;

                    otherLang.Language = "ru";
                    transId = db.InsertTranslation(otherLang);
                    if (transId == null)
                        status = false;
                }
                else
                {
                    status = false;
                }


                if (status && hasImage)
                {
                    var productImages = item.Element("Images").Elements("Image");

                    foreach (XElement prodImage in productImages)
                    {
                        PictureEntity picEntity = new PictureEntity();

                        picEntity.CreationDate = DateTime.Now;
                        picEntity.SeoName = prodImage.Element("SeoName").Value;
                        picEntity.RealPath = prodImage.Element("RealPath").Value;
                        picEntity.FullPath = prodImage.Element("FullPath").Value;
                        picEntity.RealName = prodImage.Element("RealName").Value;

                        PictureDbProxy picDb = new PictureDbProxy();
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
                }
            }
            return status;
        }
        public bool SaveCategoryWithPictures(XDocument xml)
        {
            bool status = true;

            var productList = xml.Element("Categories").Elements("Category");

            foreach (XElement item in productList)
            {

                bool hasImage = false;
                if (item.Element("CreationDate") != null && item.Element("PicId") != null)
                    hasImage = true;

                CategoryEntity category = new CategoryEntity();
                category.DisplayOrder = Int32.Parse(item.Element("DisplayOrder").Value);
                category.Id = Int32.Parse(item.Element("Id").Value);
                category.ParentId = Int32.Parse(item.Element("ParentId").Value);
                category.ShowOnHomePage = bool.Parse(item.Element("ShowOnHomePage").Value);
                category.CreationDate = DateTime.Now;

                category.Translation = new CategoryTranslationEntity();
                category.Translation.Name = item.Element("Name").Value;

                category.Published = true;

                CategoryDbProxy db = new CategoryDbProxy();
                int? cat_id = null;
                if (hasImage)
                {
                    var productImages = item.Element("Images").Elements("Image");

                    foreach (XElement prodImage in productImages)
                    {
                        PictureEntity picEntity = new PictureEntity();

                        picEntity.CreationDate = DateTime.Now;
                        picEntity.SeoName = prodImage.Element("SeoName").Value;
                        picEntity.RealPath = prodImage.Element("RealPath").Value;
                        picEntity.FullPath = prodImage.Element("FullPath").Value;
                        picEntity.RealName = prodImage.Element("RealName").Value;

                        PictureDbProxy picDb = new PictureDbProxy();
                        int? curPicId = picDb.Insert(picEntity);

                        if (curPicId == null)
                        {
                            status = false;
                        }
                        else
                        {
                            category.PictureId = curPicId.Value;

                            cat_id = db.Insert(category);

                            if (cat_id == null)
                            {
                                status = false;
                                break;
                            }
                        }
                    }
                }

                if (cat_id != null)
                {
                    category.Translation.CategoryId = cat_id.Value;
                    category.Translation.Language = "hy";
                    int? transId = db.InsertTranslation(category.Translation);
                    if (transId == null)
                        status = false;

                    CategoryTranslationEntity otherLang = new CategoryTranslationEntity();
                    otherLang.CategoryId = cat_id.Value;
                    otherLang.Language = "en";
                    transId = db.InsertTranslation(otherLang);
                    if (transId == null)
                        status = false;

                    otherLang.Language = "ru";
                    transId = db.InsertTranslation(otherLang);
                    if (transId == null)
                        status = false;
                }
                else
                {
                    status = false;
                }


            }
            return status;
        }

        public bool MapProductsToCategories()
        {
            bool status = true;

            List<ProductCategoryMappingEntity> p_to_cList = new List<ProductCategoryMappingEntity>();

            p_to_cList = new TempDbProxy().GetProductsToCategories();

            List<string> SkuList = new List<string>();

            foreach (ProductCategoryMappingEntity item in p_to_cList)
            {
                SkuList.Add("'" + item.Sku.ToString() + "'");
            }
            string SkusJoined = string.Join(",", SkuList);
            
            List< ProductCategoryMappingEntity> newProductMappingList = new ProductDbProxy().GetProductsIdAndSkuListSkuList(SkusJoined);

            List<string> oldIdList = new List<string>();

            foreach (ProductCategoryMappingEntity item in newProductMappingList)
            {
                foreach (ProductCategoryMappingEntity _item in p_to_cList)
                {
                    if (_item.Sku == item.Sku)
                    {
                        item.ProductOldid = _item.ProductOldid;
                        oldIdList.Add(item.CategoryOldId.ToString());
                    }
                }
            }

            string OldIdsJoined = string.Join(",", oldIdList);

            List<ProductCategoryMappingEntity> preFinalProductMappingList = new CategoryDbProxy().GetCategoryRealIdsFromOldIds(OldIdsJoined);

            List<ProductCategoryMappingEntity> finalProductMappingList = new List<ProductCategoryMappingEntity>();

            foreach (ProductCategoryMappingEntity item in newProductMappingList)
            {
                foreach (ProductCategoryMappingEntity _item in preFinalProductMappingList)
                {
                    if (_item.CategoryOldId == item.CategoryOldId)
                    {
                        item.CategoryId = _item.CategoryId;
                        finalProductMappingList.Add(item);

                        ProductToCategory mapEntity = new ProductToCategory();
                        mapEntity.CategoryId = item.CategoryId;
                        mapEntity.ProductId = item.Productid;

                        int? mapId = new ProductDbProxy().InsertProductCateogry(mapEntity);

                        if(mapId == null)
                        {
                            status = false;
                        }
                    }
                }
            }

            return status;
        }
    }
}
