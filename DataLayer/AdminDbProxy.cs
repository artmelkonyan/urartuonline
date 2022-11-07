using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class AdminDbProxy : BaseDbProxy
    {
        public bool AddProductsFromXml(List<ProductEntity> producs)
        {
            return true;
        }
        public List<ProductEntity> GetDashboardProducts(int curPage, int pageSize)
        {
            List<ProductEntity> p_list = new List<ProductEntity>();
            this.Open();
            SqlCommand cmd = new SqlCommand("GetDashboardsProducts", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                if (curPage < 1)
                {
                    curPage = 1;
                }
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CurrentPage", curPage);
                if (pageSize > 1)
                {
                    cmd.Parameters.AddWithValue("PageSize", pageSize);
                }

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        p_list.Add(new ProductEntity
                        {
                            Published = (bool)row["Published"],
                            Translation = new ProductTranslationEntity
                            {
                                NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Id = (int)row["Id"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            Count = (int)row["count"],
                            MainImage = new PictureEntity
                            {
                                FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            }
                        });
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }

            finally
            {
                this.Close();
            }

            return p_list;
        }
        public List<ProductEntity> GetAllProducts()
        {
            List<ProductEntity> p_list = new List<ProductEntity>();
            this.Open();
            SqlCommand cmd = new SqlCommand("GetAllProducts", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        p_list.Add(new ProductEntity
                        {
                            Published = (bool)row["Published"],
                            Translation = new ProductTranslationEntity
                            {
                                NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Id = (int)row["Id"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            Count = (int)row["count"],
                            Price=(decimal)row["Price"],
                            OldPrice=(decimal?)row["OldPrice"],
                            MainImage = new PictureEntity
                            {
                                FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            }
                        });
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }

            finally
            {
                this.Close();
            }

            return p_list;
        }

        public List<OrderEntity> GetDashboardOrders(int curPage, int pageSize)
        {
            List<OrderEntity> o_list = new List<OrderEntity>();
            this.Open();

            SqlCommand cmd = new SqlCommand("GetDashboardOrders", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                if (curPage < 1)
                {
                    curPage = 1;
                }
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CurrentPage", curPage);
                if (pageSize > 1)
                {
                    cmd.Parameters.AddWithValue("PageSize", pageSize);
                }

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        o_list.Add(new OrderEntity
                        {
                            Id = (int)row["Id"],
                            ShipmentStatus = (byte)row["ShippingStatus"],
                            TotalMoney = Convert.ToInt32((decimal)row["TotalMoney"]),
                            CreationDate = (DateTime)row["CreateDate"],
                            FirstName = (row["FirstName"] == DBNull.Value) ? null : (string)row["FirstName"],
                            LastName = (row["LastName"] == DBNull.Value) ? null : (string)row["LastName"],
                            Address = (row["Address"] == DBNull.Value) ? null : (string)row["Address"],
                            PaymentMethod= (bool?)row["Paymentmetod"]
                        });
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {
                this.Close();
            }
            return o_list;
        }

        public List<CategoryEntityDashboardViewModel> GetDashboardsCategoriesSearch(int catPage, int catPagesize, string searchName)
        {
            List<CategoryEntityDashboardViewModel> categories = new List<CategoryEntityDashboardViewModel>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetDashboardsCategoriesSearch", this.mConnection);

            try
            {

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;

                if (catPage < 1)
                {
                    catPage = 1;
                }

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CurrentPage", catPage);
                cmd.Parameters.AddWithValue("Search", searchName);

                if (catPagesize > 1)
                {
                    cmd.Parameters.AddWithValue("PageSize", catPagesize);
                }

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        categories.Add(new CategoryEntityDashboardViewModel
                        {
                            Published = (bool)row["Published"],
                            Translation = new CategoryTranslationEntityViewModel
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Id = (int)row["Id"],
                            ParentCategoryNameHy = (row["ParentName"] == DBNull.Value) ? null : (string)row["ParentName"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Picture = new PictureEntityViewModel
                            {
                                FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            }
                        });
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {

                this.Close();
            }

            return categories;
        }
        public List<CategoryEntityDashboardViewModel> GetDashboardgategories(int catPage, int catPagesize)
        {
            List<CategoryEntityDashboardViewModel> categories = new List<CategoryEntityDashboardViewModel>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetDashboardsCategories", this.mConnection);

            try
            {

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;

                if (catPage < 1)
                {
                    catPage = 1;
                }

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CurrentPage", catPage);
                if (catPagesize > 1)
                {
                    cmd.Parameters.AddWithValue("PageSize", catPagesize);
                }

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        categories.Add(new CategoryEntityDashboardViewModel
                        {
                            Published = (bool)row["Published"],
                            Translation = new CategoryTranslationEntityViewModel
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Id = (int)row["Id"],
                            ParentCategoryNameHy = (row["ParentName"] == DBNull.Value) ? null : (string)row["ParentName"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Picture = new PictureEntityViewModel
                            {
                                FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            }
                        });
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {

                this.Close();
            }

            return categories;
        }
        public ProductEntity GetProductBySku(string sku)
        {
            ProductEntity product = new ProductEntity();

            product.MainImage = new PictureEntity();
            product.Translation = new ProductTranslationEntity();
            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsBySku", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.AddWithValue("sku", sku);
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        product = new ProductEntity
                        {
                            Published = (bool)dt.Rows[0]["Published"],
                            Translation = new ProductTranslationEntity
                            {
                                NameTranslation = (dt.Rows[0]["Name"] == DBNull.Value) ? null : (string)dt.Rows[0]["Name"],
                            },
                            Id = (int)dt.Rows[0]["Id"],
                            Sku = (dt.Rows[0]["Sku"] == DBNull.Value) ? null : (string)dt.Rows[0]["Sku"],
                            Count = (int)dt.Rows[0]["count"],
                            MainImage = new PictureEntity
                            {
                                FullPath = (dt.Rows[0]["FullPath"] == DBNull.Value) ? null : (string)dt.Rows[0]["FullPath"],
                            }
                        };
                    }
                }
                cmd.Dispose();
            }
            catch (Exception)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {
                this.Close();
            }

            return product;
        }
        public List<CategoryEntityDashboardViewModel> GetProductCatList()
        {
            List<CategoryEntityDashboardViewModel> c_list = new List<CategoryEntityDashboardViewModel>();
            this.Open();

            SqlCommand cmd = new SqlCommand("GetCategories", this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        c_list.Add(new CategoryEntityDashboardViewModel
                        {
                            Translation = new CategoryTranslationEntityViewModel
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Id = (int)row["Id"],
                        });
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {

                this.Close();
            }
            return c_list;
        }
        public ProductEditViewModel GetProductById(int? id)
        {
            PictureDbProxy dbPic = new PictureDbProxy();
            ProductEditViewModel product = new ProductEditViewModel();
            List<PictureEntityViewModel> p_list = new List<PictureEntityViewModel>();
            var picList = dbPic.GetProductPictures(id);
            foreach (var item in picList)
            {
                p_list.Add(new PictureEntityViewModel
                {
                    Id = item.Id.Value,
                    FullPath = item.FullPath,
                    Main = item.Main.Value,
                    Status = false
                });
            }
            var productcategorylist = new List<CategoryEntityDashboardViewModel>()
            {
                new CategoryEntityDashboardViewModel()
                {
                    Id=0,
                    Translation=new CategoryTranslationEntityViewModel()
                    {
                        Name="----"
                    }
                }
            };
            productcategorylist.AddRange(this.GetProductCatList());
            this.Open();
            SqlCommand cmd = new SqlCommand("GetProductDashboardById", this.mConnection);
            var transList = new List<ProductTranslationEntityViewModel>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Id", id.Value);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        transList.Add(new ProductTranslationEntityViewModel
                        {
                            Id = (int)dt.Rows[i]["TranslationId"],
                            Language = (string)dt.Rows[i]["Language"],
                            FullDescriptionTranslation = (dt.Rows[i]["FullDescription"] == DBNull.Value) ? null : (string)dt.Rows[i]["FullDescription"],
                            NameTranslation = (dt.Rows[i]["Name"] == DBNull.Value) ? null : (string)dt.Rows[i]["Name"],
                            ShortDescriptionTranslation = (dt.Rows[i]["ShortDescription"] == DBNull.Value) ? null : (string)dt.Rows[i]["ShortDescription"],
                            ProductId = (int)dt.Rows[i]["Id"],
                        });
                    }

                    product = new ProductEditViewModel
                    {
                        Id = (int)dt.Rows[0]["Id"],
                        ProductsCategoryList = productcategorylist,
                        CategoryId = (dt.Rows[0]["CategoryId"] == DBNull.Value) ? 0 : (int)dt.Rows[0]["CategoryId"],
                        Count = (int)dt.Rows[0]["Count"],
                        OldPrice = (decimal)dt.Rows[0]["OldPrice"],
                        PictureList = p_list,
                        Published = (bool)dt.Rows[0]["Published"],
                        Price = (decimal)dt.Rows[0]["Price"],
                        BrandId=(int)(dt.Rows[0]["BrandId"]==DBNull.Value?0: dt.Rows[0]["BrandId"]),
                        ShowOnHomePage = (bool)dt.Rows[0]["ShowOnHomePage"],
                        TranslationList = transList,
                        Sku = (dt.Rows[0]["sku"] == DBNull.Value) ? null : (string)dt.Rows[0]["sku"],
                        MainImage = new PictureEntityViewModel
                        {
                            FullPath = (dt.Rows[0]["FullPath"] == DBNull.Value) ? null : (string)dt.Rows[0]["FullPath"],
                            Id = (dt.Rows[0]["PictureId"] == DBNull.Value) ? 0 : (int)dt.Rows[0]["PictureId"],
                            Status = (dt.Rows[0]["Published"] == DBNull.Value) ? false : (bool)dt.Rows[0]["Published"],
                        }
                    };
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {

                this.Close();
            }

            return product;
        }
        public void EditOrder(OrderEntityViewModel mdl)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand("UpdateOrder", this.mConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                cmd.Parameters.AddWithValue("OrderId", mdl.Id);
                cmd.Parameters.AddWithValue("ShippingStatus", mdl.ShipmentStatus);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();

            }
            finally
            {
                this.Close();
            }

        }
        public CategoryEntityDashboardViewModel GetCategoryById(int? Id)
        {
            List<CategoryTranslationEntityViewModel> trlist = new List<CategoryTranslationEntityViewModel>();
            List<CategoryEntityDashboardViewModel> catLsit = new List<CategoryEntityDashboardViewModel>() { new CategoryEntityDashboardViewModel() {
            Id=0,
            Translation=new CategoryTranslationEntityViewModel()
                            {
                                Name="Անծնող կատեգորիյա"
                            }
                }
            };
            catLsit.AddRange(this.GetProductCatList());

            CategoryEntityDashboardViewModel category = new CategoryEntityDashboardViewModel();

            this.Open();
            SqlCommand cmd = new SqlCommand("GetCategoryDashboardById", this.mConnection);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Id", Id.Value);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var lang = ((row["Language"] == DBNull.Value) ? "" : (string)row["Language"]);
                        if (!trlist.Any(x => x.Language == lang))
                            trlist.Add(new CategoryTranslationEntityViewModel
                            {
                                Id = (row["TranslationId"] == DBNull.Value) ? 0 : (int)row["TranslationId"],
                                Language = (row["Language"] == DBNull.Value) ? null : (string)row["Language"],
                                DescriptionTranslation = (row["Description"] == DBNull.Value) ? null : (string)row["Description"],
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"]
                            });

                        category = new CategoryEntityDashboardViewModel
                        {
                            Id = (int)dt.Rows[0]["Id"],
                            ParentId = (dt.Rows[0]["ParentId"] == DBNull.Value) ? 0 : (int)dt.Rows[0]["ParentId"],
                            Published = (bool)dt.Rows[0]["Published"],
                            Picture = new PictureEntityViewModel
                            {
                                Id = (dt.Rows[0]["PictureId"] == DBNull.Value) ? 0 : (int)dt.Rows[0]["PictureId"],
                                FullPath = (dt.Rows[0]["FullPath"] == DBNull.Value) ? null : (string)dt.Rows[0]["FullPath"]
                            },
                            Translation = new CategoryTranslationEntityViewModel
                            {
                                Id = (dt.Rows[0]["ParentId"] == DBNull.Value) ? 0 : (int)dt.Rows[0]["ParentId"],
                                Name = (row["ParentName"] == DBNull.Value) ? null : (string)row["ParentName"]
                            },
                            ShowOnHomePage = (bool)dt.Rows[0]["ShowOnHomePage"],
                            ShowOnLayout = (bool)dt.Rows[0]["ShowOnLayout"],
                            TranslationList = trlist,
                            CategoriesList = catLsit,
                            Status = (bool)dt.Rows[0]["Status"]
                        };
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {

                this.Close();
            }

            return category;
        }
        public void EditProduct(ProductEditViewModel entity, IFormFile mainImage, IFormFileCollection files)
        {

        }
        public void EditCateGory(CategoryEntityDashboardViewModel model)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand("UpdateCategory", this.mConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                int? picId = null;
                if (model.PictureId != 0)
                    picId = model.PictureId;
                int? parentCategoryId = null;
                if (model.ParentId != 0)
                    parentCategoryId = model.ParentId;
                cmd.Parameters.AddWithValue("Id", model.Id);
                cmd.Parameters.AddWithValue("PictureId", picId);
                cmd.Parameters.AddWithValue("ParentId", parentCategoryId);
                cmd.Parameters.AddWithValue("Published", model.Published);
                cmd.Parameters.AddWithValue("DisplayOrder", model.DisplayOrder);
                cmd.Parameters.AddWithValue("Status", true);
                cmd.Parameters.AddWithValue("ShowOnHomePage", model.ShowOnHomePage);
                cmd.Parameters.AddWithValue("ShowOnLayout", model.ShowOnLayout);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();

            }
            finally
            {
                this.Close();
            }
        }
        public void EditCategoryTranslation(CategoryTranslationEntityViewModel model)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand("UpdateCategoryTranslation", this.mConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                cmd.Parameters.AddWithValue("Id", model.Id);
                cmd.Parameters.AddWithValue("Name", model.Name);
                cmd.Parameters.AddWithValue("SeoName", model.SeoName);
                cmd.Parameters.AddWithValue("Description", model.DescriptionTranslation);
                cmd.Parameters.AddWithValue("Status", model.Status);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();

            }
            finally
            {
                this.Close();
            }
        }

        public bool UpdateCountBySkuList(string skuJoined)
        {
            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();


            string queryString = "Update [dbo].[Products] SET [Count] = 0"
                + "  WHERE [Sku] not IN (" + skuJoined + ")";

            SqlCommand cmd = new SqlCommand(queryString, this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                cmd.Dispose();

                return dt != null;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return false;
            }
            finally
            {
                this.Close();
            }
        }


        public List<ProductEntity> GetProductsBySkuList(string skuJoined)
        {
            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();


            string queryString = @"
    Select p.*,
	PoTr.Name 
	
    FROM Products as p
    JOIN ProductTranslation as PoTr ON PoTr.ProductId = p.Id
    WHERE PoTr.Language = 'hy' AND p.sku  in ("+ skuJoined + ")";

            SqlCommand cmd = new SqlCommand(queryString, this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ProductEntity CurItem;
                        CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"]
                        };

                        //CurItem.Translation = new ProductTranslationEntity
                        //{
                        //    FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                        //    NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                        //    SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                        //    ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        //};


                    

                        list.Add(CurItem);
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {
                this.Close();
            }

            return list;
        }

        public List<ProductEntity> GetProductsByExcludedSkuList(string skuJoined)
        {
            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();


            string queryString = "SELECT "
        + " p.*, pics.FullPath, p_t.FullDescription, p_t.[Language], p_t.[Name], p_t.SeoName, p_t.ShortDescription FROM"
        + " [dbo].[Products] p INNER JOIN(Select* FROM [dbo].[ProductTranslation]"
            + " WHERE [dbo].[ProductTranslation].[Language] = 'hy') p_t ON p.Id = p_t.ProductId"
        + " LEFT JOIN(select* FROM Product_To_Picture"
        + " ) ptp ON p.Id = ptp.ProductId AND ptp.[Status] = 1"
        + " LEFT JOIN(Select* from Pictures) pics ON pics.Id = ptp.PictureId AND pics.Main = 1"
    + " WHERE "
        + " p.[Status] = 1";
              queryString = @"SELECT *
    FROM [Products] p";

            SqlCommand cmd = new SqlCommand(queryString, this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ProductEntity CurItem;
                        CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count=(int)row["Count"]
                        };

                        //CurItem.Translation = new ProductTranslationEntity
                        //{
                        //    FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                        //    NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                        //    SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                        //    ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        //};


                        //CurItem.MainImage = new PictureEntity
                        //{
                        //    FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        //};

                        list.Add(CurItem);
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return null;
            }
            finally
            {
                this.Close();
            }

            return list;
        }
    }
}
