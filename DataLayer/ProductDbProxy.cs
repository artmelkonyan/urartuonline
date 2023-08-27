using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class ProductLayerHelper
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
    public class ProductDbProxy : BaseDbProxy
    {
        
        public bool UpdateProduct(ProductEntity entity)
        {
            bool status = true;

            this.Open();

            SqlCommand cmd = new SqlCommand("UpdateProductBySku", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Sku", entity.Sku);
                cmd.Parameters.AddWithValue("Price", entity.Price);
                cmd.Parameters.AddWithValue("OldPrice", entity.OldPrice);
                cmd.Parameters.AddWithValue("Published", entity.Published);
                cmd.Parameters.AddWithValue("BrandId", entity.BrandId == 0 ? null : entity.BrandId);
                cmd.Parameters.AddWithValue("Count", entity.Count);
                cmd.Parameters.AddWithValue("ShowOnHomePage", entity.ShowOnHomePage);
                cmd.Parameters.AddWithValue("Status", entity.Status);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt == null)
                {
                    status = false;
                }
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
            return status;
        }

        public int? InsertOrder(OrderEntityViewModel model, string username)
        {

            this.Open();
            SqlCommand cmd = new SqlCommand("SetOrder", this.mConnection);
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                if (username != null)
                {
                    cmd.Parameters.AddWithValue("UserName", username);
                }
                if (model.FirstName != null)
                {
                    cmd.Parameters.AddWithValue("FirstName", model.FirstName);
                }
                if (model.LastName != null)
                {
                    cmd.Parameters.AddWithValue("LastName", model.LastName);
                }
                if (model.Phone != null)
                {
                    cmd.Parameters.AddWithValue("Phone", model.Phone);
                }
                if (model.OrderComment != null)
                {
                    cmd.Parameters.AddWithValue("OrderComment", model.OrderComment);
                }
                if (model.BankOrderId != null)
                {
                    cmd.Parameters.AddWithValue("BankOrderId", model.BankOrderId);
                }
                if (model.Email != null)
                {
                    cmd.Parameters.AddWithValue("Email", model.Email);
                }
                if (model.Address != null)
                {
                    cmd.Parameters.AddWithValue("Address", model.Address);
                }

                cmd.Parameters.AddWithValue("Paymentmetod", model.PaymentMethod);
                cmd.Parameters.AddWithValue("TotalMoney", model.TotalMoney);
                cmd.Parameters.AddWithValue("ShipmentMethod", model.ShipmentMetod);

                SqlParameter outputIdParam = new SqlParameter("InsertedId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);
                cmd.ExecuteNonQuery();

                var InsertedId = outputIdParam.Value as int?;
                cmd.Dispose();
                if (InsertedId.Value > 0)
                {
                    foreach (var prod in model.OrderedProducts)
                    {
                        cmd = new SqlCommand("SetOrderProducts", this.mConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@OrderId", InsertedId.Value);
                        cmd.Parameters.AddWithValue("ProductId", prod.Id);
                        cmd.Parameters.AddWithValue("ProductPrice", prod.Price);
                        cmd.Parameters.AddWithValue("ProductCount", prod.Count);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    return InsertedId;
                }
            }
            catch (Exception ex)
            {
                cmd.Dispose();

            }
            finally
            {
                this.Close();
            }

            return 0;
        }
        public List<ProductEditViewModel> GetProductPrices(string idlist)
        {
            List<ProductEditViewModel> list = new List<ProductEditViewModel>();
            this.Open();

            SqlCommand cmd = new SqlCommand("SELECT Id, Price, Sku,[Count] FROM Products WHERE Id IN(" + idlist + ")", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.Text;
                SqlDataReader da = cmd.ExecuteReader();

                if (da.HasRows)
                {
                    while (da.Read())
                    {
                        list.Add(new ProductEditViewModel
                        {
                            Id = (int)da["Id"],
                            Price = (decimal)da["Price"],
                            Count = (int)da["Count"],
                            Sku = (string)da["Sku"]
                        });

                    }
                }
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
            return list;
        }
        public Tuple<int, List<ProductEntity>> GetProductsByCategoryIdAndBrandListId(int[] brandId, int categoryId, int CurrentPage, int ViewCount, string RequestLanguage, string order = null)
        {
            Tuple<int, List<ProductEntity>> tuple;

            List<ProductEntity> list = new List<ProductEntity>();
            order = order ?? "name";
            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsByCategoryAndBrandListId", this.mConnection);
            int? TotalCount = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                string joined = string.Join(",", brandId);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BrandId", joined);
                cmd.Parameters.AddWithValue("CategoryId", categoryId);
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("CurrentPage", CurrentPage);
                cmd.Parameters.AddWithValue("ViewCount", ViewCount);
                cmd.Parameters.AddWithValue("Order", order);



                SqlParameter outputIdParam = new SqlParameter("TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);


                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                TotalCount = outputIdParam.Value as int?;

                if (TotalCount == null)
                    TotalCount = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"],
                            BrandId = (int)row["BrandId"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };

                        if (CurItem.Count < 1)
                            continue;

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
            tuple = new Tuple<int, List<ProductEntity>>(TotalCount.Value, list);

            return tuple;
        }
        public Tuple<int, List<ProductEntity>> GetProductsByBrandListId(int[] brandId, int CurrentPage, int ViewCount, string RequestLanguage, string order = null)
        {
            Tuple<int, List<ProductEntity>> tuple;

            List<ProductEntity> list = new List<ProductEntity>();
            order = order ?? "name";
            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsByBrandListId", this.mConnection);
            int? TotalCount = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                if (CurrentPage <= 0)
                    CurrentPage = 1;
                da.SelectCommand = cmd;
                string joined = string.Join(",", brandId);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BrandId", joined);
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("CurrentPage", CurrentPage);
                cmd.Parameters.AddWithValue("ViewCount", ViewCount);
                cmd.Parameters.AddWithValue("Order", order);



                SqlParameter outputIdParam = new SqlParameter("TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);


                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                TotalCount = outputIdParam.Value as int?;

                if (TotalCount == null)
                    TotalCount = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"],
                            BrandId = (int)row["BrandId"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };

                        if (CurItem.Count < 1)
                            continue;

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
            tuple = new Tuple<int, List<ProductEntity>>(TotalCount.Value, list);

            return tuple;
        }
        public List<ProductEntity> GetProductByIdList(string idList)
        {
            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsByIdList", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("IdList", idList);
                cmd.Parameters.AddWithValue("Language", "hy");
                cmd.Parameters.AddWithValue("Status", true);
                cmd.Parameters.AddWithValue("Published", true);

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
                            Count = (int)row["Count"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };


                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };

                        list.Add(CurItem);
                        cmd.Dispose();
                    }
                }
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
        public List<ProductCategoryMappingEntity> GetProductsIdAndSkuListSkuList(string SkuList)
        {
            List<ProductCategoryMappingEntity> list = new List<ProductCategoryMappingEntity>();

            this.Open();

            string queryString = "select Products.Id, Products.Sku"
                + " From Products"
                + " Where Products.Sku in (" + SkuList + ")";

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
                        if ((row["Sku"] != DBNull.Value))
                        {
                            ProductCategoryMappingEntity entity = new ProductCategoryMappingEntity();
                            entity.Productid = (int)row["Id"];
                            entity.Sku = (string)row["Sku"];
                            list.Add(entity);
                        }
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

        public bool Insert(List<ProductEntity> list)
        {
            this.Open();

            SqlCommand cmd = new SqlCommand("ProductInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                foreach (var itm in list)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Price", itm.Price);
                    cmd.Parameters.AddWithValue("OldPrice", itm.OldPrice);
                    cmd.Parameters.AddWithValue("Count", itm.Count);
                    cmd.Parameters.AddWithValue("Sku", itm.Sku);

                    DataTable dt = new DataTable();

                    // Set the data adapter’s select command
                    da.Fill(dt);

                    if (dt != null)
                    {

                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                cmd.Dispose();
            }
            finally
            {
                this.Close();

            }
            return true;
        }

        public bool UpdateProductTranslation(ProductTranslationEntity entity)
        {
            bool status = true;

            this.Open();

            SqlCommand cmd = new SqlCommand("UpdateProductTranslation", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", entity.Id);
                cmd.Parameters.AddWithValue("Name", entity.NameTranslation);
                cmd.Parameters.AddWithValue("ShortDescription", entity.ShortDescriptionTranslation);
                cmd.Parameters.AddWithValue("FullDescription", entity.FullDescriptionTranslation);
                cmd.Parameters.AddWithValue("SeoName", entity.SeoName);
                cmd.Parameters.AddWithValue("Status", entity.Status);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt == null)
                {
                    status = false;
                }
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
            return status;
        }
        public int? Insert(ProductEntity itm)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("ProductInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Price", itm.Price);
                cmd.Parameters.AddWithValue("OldPrice", itm.OldPrice);
                cmd.Parameters.AddWithValue("Count", itm.Count);
                cmd.Parameters.AddWithValue("Sku", itm.Sku);
                cmd.Parameters.AddWithValue("CreationDate", itm.CreationDate??DateTime.Now);
                cmd.Parameters.AddWithValue("Published", itm.Published);



                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                try
                {

                    da.Fill(dt);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    dt = null;
                }

                if (dt != null)
                {
                    decimal decId = (decimal)dt.Rows[0][0];

                    id = decimal.ToInt32(decId);
                }
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
            return id;
        }


        public int? InsertProductCateogry(ProductToCategory itm)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("ProductCategoryInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ProductId", itm.ProductId);
                cmd.Parameters.AddWithValue("CategoryId", itm.CategoryId);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    decimal decId = (decimal)dt.Rows[0][0];

                    id = decimal.ToInt32(decId);
                }
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
            return id;
        }

        public int? InsertTranslation(ProductTranslationEntity itm)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("ProductTranslationInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ProductId", itm.ProductId);
                cmd.Parameters.AddWithValue("Language", itm.Language);
                cmd.Parameters.AddWithValue("NameTranslation", itm.NameTranslation);
                cmd.Parameters.AddWithValue("ShortDescriptionTranslation", itm.ShortDescriptionTranslation);
                cmd.Parameters.AddWithValue("FullDescriptionTranslation", itm.FullDescriptionTranslation);
                cmd.Parameters.AddWithValue("SeoName", itm.SeoName);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    decimal decId = (decimal)dt.Rows[0][0];

                    id = decimal.ToInt32(decId);
                }
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
            return id;
        }

        public List<ProductEntity> GetProductList()
        {
            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductList", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Language", "hy");

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

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            Id = (int)row["TranslationId"],
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };


                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };

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
        public Tuple<int, List<ProductEntity>> SearchByBrandNameAndProductName(string brandSearchWord, string searchword, string searchwordFixed, int CurrentPage, int ViewCount, string RequestLanguage, string Order)
        {
            Tuple<int, List<ProductEntity>> tuple;

            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsBrandBySearch", this.mConnection);
            int? TotalCount = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BrandSearchWord", brandSearchWord);
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("SearchWord", searchword);
                cmd.Parameters.AddWithValue("SearchWordFixed", searchwordFixed == null ? "" : searchwordFixed);
                cmd.Parameters.AddWithValue("CurrentPage", CurrentPage);
                cmd.Parameters.AddWithValue("ViewCount", ViewCount);
                cmd.Parameters.AddWithValue("Order", Order);



                SqlParameter outputIdParam = new SqlParameter("TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);


                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                TotalCount = outputIdParam.Value as int?;

                if (TotalCount == null)
                    TotalCount = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };



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
            list = list.DistinctBy(p => p.Id).ToList();
            tuple = new Tuple<int, List<ProductEntity>>(TotalCount.Value, list);

            return tuple;


        }
        public Tuple<int, List<ProductEntity>> SearchByBrandName(string searchword, string searchwordFixed, int CurrentPage, int ViewCount, string RequestLanguage, string Order)
        {
            Tuple<int, List<ProductEntity>> tuple;

            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsByBrandNameSearch", this.mConnection);
            int? TotalCount = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("SearchWord", searchword);
                cmd.Parameters.AddWithValue("SearchWordFixed", searchwordFixed == null ? "" : searchwordFixed);
                cmd.Parameters.AddWithValue("CurrentPage", CurrentPage);
                cmd.Parameters.AddWithValue("ViewCount", ViewCount);
                cmd.Parameters.AddWithValue("Order", Order);



                SqlParameter outputIdParam = new SqlParameter("TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);


                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                TotalCount = outputIdParam.Value as int?;

                if (TotalCount == null)
                    TotalCount = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };



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
            list = list.DistinctBy(p => p.Id).ToList();
            tuple = new Tuple<int, List<ProductEntity>>(TotalCount.Value, list);

            return tuple;


        }
        public Tuple<int, List<ProductEntity>> Search(string searchword, string searchwordFixed, int CurrentPage, int ViewCount, string RequestLanguage, string Order)
        {
            Tuple<int, List<ProductEntity>> tuple;

            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsBySearch", this.mConnection);
            int? TotalCount = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("SearchWord", searchword);
                cmd.Parameters.AddWithValue("SearchWordFixed", searchwordFixed == null ? "" : searchwordFixed);
                cmd.Parameters.AddWithValue("CurrentPage", CurrentPage);
                cmd.Parameters.AddWithValue("ViewCount", ViewCount);
                cmd.Parameters.AddWithValue("Order", Order);



                SqlParameter outputIdParam = new SqlParameter("TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);


                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                TotalCount = outputIdParam.Value as int?;

                if (TotalCount == null)
                    TotalCount = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };



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
            list = list.DistinctBy(p => p.Id).ToList();
            tuple = new Tuple<int, List<ProductEntity>>(TotalCount.Value, list);

            return tuple;


        }
        public Tuple<int, List<ProductEntity>> GetByBrandId(int brandId, int CurrentPage, int ViewCount, string RequestLanguage, string order = null)
        {
            Tuple<int, List<ProductEntity>> tuple;

            List<ProductEntity> list = new List<ProductEntity>();
            order = order ?? "name";
            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsByBrandId", this.mConnection);
            int? TotalCount = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BrandId", brandId);
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("CurrentPage", CurrentPage);
                cmd.Parameters.AddWithValue("ViewCount", ViewCount);
                cmd.Parameters.AddWithValue("Order", order);



                SqlParameter outputIdParam = new SqlParameter("TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);


                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                TotalCount = outputIdParam.Value as int?;

                if (TotalCount == null)
                    TotalCount = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"],
                            BrandId = (int)row["BrandId"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };

                        if (CurItem.Count < 1)
                            continue;

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
            tuple = new Tuple<int, List<ProductEntity>>(TotalCount.Value, list);

            return tuple;
        }
        public Tuple<int, List<ProductEntity>> GetProductListForUser(int CategoryId, int CurrentPage, int ViewCount, string OrderBy, string RequestLanguage)
        {
            Tuple<int, List<ProductEntity>> tuple;

            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsByCategoryId", this.mConnection);
            int? TotalCount = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CategoryId", CategoryId);
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("CurrentPage", CurrentPage);
                cmd.Parameters.AddWithValue("ViewCount", ViewCount);



                SqlParameter outputIdParam = new SqlParameter("TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                cmd.Parameters.AddWithValue("Order", OrderBy);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                TotalCount = outputIdParam.Value as int?;

                if (TotalCount == null)
                    TotalCount = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };

                        if (CurItem.Count < 1)
                            continue;

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
            tuple = new Tuple<int, List<ProductEntity>>(TotalCount.Value, list);

            return tuple;


        }
        public Tuple<int, List<ProductEntity>> GetProductListForUser(int brandId, int CategoryId, int CurrentPage, int ViewCount, string OrderBy, string RequestLanguage)
        {
            OrderBy = OrderBy ?? "name";
            Tuple<int, List<ProductEntity>> tuple;

            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductsByCategoryAndBrandId", this.mConnection);
            int? TotalCount = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CategoryId", CategoryId);
                cmd.Parameters.AddWithValue("BrandId", brandId);
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("CurrentPage", CurrentPage);
                cmd.Parameters.AddWithValue("ViewCount", ViewCount);



                SqlParameter outputIdParam = new SqlParameter("TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                cmd.Parameters.AddWithValue("Order", OrderBy);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                TotalCount = outputIdParam.Value as int?;

                if (TotalCount == null)
                    TotalCount = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        CurItem.MainImage = new PictureEntity
                        {
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"]
                        };

                        if (CurItem.Count < 1)
                            continue;

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
            tuple = new Tuple<int, List<ProductEntity>>(TotalCount.Value, list);

            return tuple;


        }

        public ProductEntity GetProductById(int id)
        {
            ProductEntity CurItem = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductById", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", id);
                cmd.Parameters.AddWithValue("Language", "hy");
                cmd.Parameters.AddWithValue("Status", true);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
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

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
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
            return CurItem;
        }


        public ProductEntity GetOneProductById(int? id, string RequestLanguage)
        {
            ProductEntity CurItem = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("GetOneProductById", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", id);
                cmd.Parameters.AddWithValue("Language", RequestLanguage);
                cmd.Parameters.AddWithValue("Status", true);
                cmd.Parameters.AddWithValue("Published", true);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            Count = (int)row["Count"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };
                    }
                }
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
            return CurItem;
        }

        public List<ProductEntity> GetHomePageProducts(string lang, bool? isNew = null)
        {
            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetHomePageProducts", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Language", lang);
                cmd.Parameters.AddWithValue("Status", true);
                cmd.Parameters.AddWithValue("Published", true);
                cmd.Parameters.AddWithValue("IsNew", isNew);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ProductEntity CurItem = new ProductEntity
                        {
                            Id = (int)row["Id"],
                            Count = (int)row["Count"],
                            Price = (decimal)row["Price"],
                            OldPrice = (decimal)row["OldPrice"],
                            Sku = (row["Sku"] == DBNull.Value) ? null : (string)row["Sku"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"]
                        };

                        CurItem.Translation = new ProductTranslationEntity
                        {
                            FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"],
                            NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                        };

                        list.Add(CurItem);
                    }
                }
                cmd.Dispose();
            }
            catch
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
