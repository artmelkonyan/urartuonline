using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class CategoryDbProxy : BaseDbProxy
    {
        public bool UpdateCategory(CategoryEntity entity)
        {
            bool status = true;


            this.Open();

            SqlCommand cmd = new SqlCommand("UpdateCategory", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", entity.Id);
                cmd.Parameters.AddWithValue("PictureId", entity.PictureId);
                cmd.Parameters.AddWithValue("ParentId", entity.ParentId);
                cmd.Parameters.AddWithValue("Published", entity.Published);
                cmd.Parameters.AddWithValue("DisplayOrder", entity.DisplayOrder);
                cmd.Parameters.AddWithValue("Status", entity.Status);
                cmd.Parameters.AddWithValue("ShowOnHomePage", entity.ShowOnHomePage);

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
                return status;
            }
            finally
            {
                this.Close();
            }

            return status;
        }


        public bool SetProductCategory(ProductCategoryMappingEntity entity)
        {
            bool status = true;

            this.Open();

            SqlCommand cmd = new SqlCommand("SetProductCategory", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ProductId", entity.Productid);
                cmd.Parameters.AddWithValue("CategoryId", entity.CategoryId);

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
                return status;
            }

            finally
            {
                this.Close();
            }
            return status;
        }
        public List<CategoryEntity> GetAllCategories()
        {
            List<CategoryEntity> list = new List<CategoryEntity>();
            this.Open();

            string queryString = "select *"
                + " From Categories";

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
                        CategoryEntity entity = new CategoryEntity();
                        entity.Id = (int)row["Id"];
                        entity.ParentId = (int)row["ParentId"];
                        list.Add(entity);
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
        public CategoryEntity GetCategoryById(int id, string lang)
        {
            CategoryEntity entity = new CategoryEntity();

            List<CategoryEntity> list = new List<CategoryEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetCatogoryById", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CategoryId", id);
                cmd.Parameters.AddWithValue("Language", lang);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        entity = new CategoryEntity
                        {
                            Id = (int)row["Id"],
                            PictureId = (int)row["PictureId"],
                            ParentId = (int)row["ParentId"],
                            DisplayOrder = (int)row["DisplayOrder"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            Status=(bool)row["Status"],
                            CreationDate = (DateTime)row["CreationDate"]
                        };

                        entity.Translation = new CategoryTranslationEntity
                        {
                            CategoryId = entity.Id,
                            DescriptionTranslation = (row["Description"] == DBNull.Value) ? null : (string)row["Description"],
                            Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                        };

                        entity.Picture = new PictureEntity
                        {
                            Main = (bool)row["Main"],
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"]
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
            return entity;
        }
        public List<ProductCategoryMappingEntity> GetCategoryRealIdsFromOldIds(string IdList)
        {
            List<ProductCategoryMappingEntity> list = new List<ProductCategoryMappingEntity>();

            this.Open();

            string queryString = "select Categories.Id, Categories.OldId"
                + " From Categories"
                + " Where Categories.OldId in (" + IdList + ")";

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
                        ProductCategoryMappingEntity entity = new ProductCategoryMappingEntity();
                        entity.CategoryId = (int)row["Id"];
                        list.Add(entity);
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

        public List<CategoryEntity> GetCategoryTree()
        {
            List<CategoryEntity> categories = new List<CategoryEntity>();

            categories.Add(new CategoryEntity
            {
                Id = 18,
                DisplayOrder = 1,
                Translation = new CategoryTranslationEntity
                {
                    CategoryId = 12,
                    Id = 57657,
                    Name = "ggjkhghjk"
                },
                ParentId = 9,
            });
            categories.Add(new CategoryEntity
            {
                Id = 19,
                DisplayOrder = 1,
                Translation = new CategoryTranslationEntity
                {
                    CategoryId = 12,
                    Id = 57657,
                    Name = "ggjkhghjk"
                },
                ParentId = 10,
            });
            categories.Add(new CategoryEntity
            {
                Id = 20,
                DisplayOrder = 1,
                Translation = new CategoryTranslationEntity
                {
                    CategoryId = 12,
                    Id = 57657,
                    Name = "ggjkhghjk"
                },
                ParentId = 11,
            });
            categories.Add(new CategoryEntity
            {
                Id = 21,
                DisplayOrder = 1,
                Translation = new CategoryTranslationEntity
                {
                    CategoryId = 12,
                    Id = 57657,
                    Name = "ggjkhghjk"
                },
                ParentId = 12,
            });
            categories.Add(new CategoryEntity
            {
                Id = 22,
                DisplayOrder = 1,
                Translation = new CategoryTranslationEntity
                {
                    CategoryId = 12,
                    Id = 57657,
                    Name = "ggjkhghjk"
                },
                ParentId = 12,
            });
            categories.Add(new CategoryEntity
            {
                Id = 23,
                DisplayOrder = 2,
                Translation = new CategoryTranslationEntity
                {
                    CategoryId = 12,
                    Id = 57657,
                    Name = "ggjkhghjk"
                },
                ParentId = 13,
            });
            categories.Add(new CategoryEntity
            {
                Id = 24,
                DisplayOrder = 1,
                Translation = new CategoryTranslationEntity
                {
                    CategoryId = 12,
                    Id = 57657,
                    Name = "ggjkhghjk"
                },
                ParentId = 15,
            });
            return categories;
        }

        public List<CategoryEntity> GetCategoriesByBrandId(int brandId, string lang)
        {
            List<CategoryEntity> list = new List<CategoryEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetFilterCategoriesByBrandId", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BrandId", brandId);
                cmd.Parameters.AddWithValue("Status", true);
                cmd.Parameters.AddWithValue("Published", true);
                cmd.Parameters.AddWithValue("LanguageId", lang);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var id = (int)row["Id"];
                        if (list.Any(x => x.Id == id))
                            continue;
                        var picture = int.TryParse(row["PictureId"].ToString(), out var pictureId);
                        var CurItem = new CategoryEntity
                        {
                            Id = (int)row["Id"],
                            PictureId = pictureId,
                            ParentId = (int?)row["ParentId"],
                            DisplayOrder = (int)row["DisplayOrder"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            SubItems = (row["SubItems"] == DBNull.Value) ? null : (int?)row["SubItems"]
                        };


                        CurItem.Translation = new CategoryTranslationEntity
                        {
                            CategoryId = CurItem.Id,
                            DescriptionTranslation = (row["Description"] == DBNull.Value) ? null : (string)row["Description"],
                            Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
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
            return list.Distinct().ToList();
        }
        public List<CategoryEntity> GetHompageCategories(string lang)
        {
            List<CategoryEntity> list = new List<CategoryEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetCategoriesByFilter", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ShowOnHomePage", true);
                cmd.Parameters.AddWithValue("Status", true);
                cmd.Parameters.AddWithValue("Published", true);
                cmd.Parameters.AddWithValue("Language", lang);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var picture = int.TryParse(row["PictureId"].ToString(), out var pictureId);
                        var CurItem = new CategoryEntity
                        {
                            Id = (int)row["Id"],
                            PictureId = pictureId,
                            ParentId = (int?)row["ParentId"],
                            DisplayOrder = (int)row["DisplayOrder"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"],
                            SubItems = (row["SubItems"] == DBNull.Value) ? null : (int?)row["SubItems"]
                        };


                        CurItem.Translation = new CategoryTranslationEntity
                        {
                            CategoryId = CurItem.Id,
                            DescriptionTranslation = (row["Description"] == DBNull.Value) ? null : (string)row["Description"],
                            Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                        };

                        CurItem.Picture = new PictureEntity
                        {
                            Main = (bool)(row["Main"] == DBNull.Value ? false : row["Main"]),
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"]
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

        public List<CategoryEntity> GetLayoutCategories(string lang)
        {
            List<CategoryEntity> list = new List<CategoryEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetLayoutCategories", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ShowOnLayout", true);
                cmd.Parameters.AddWithValue("Published", true);
                cmd.Parameters.AddWithValue("Language", lang);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new CategoryEntity
                        {
                            Id = (int)row["Id"],
                            PictureId = (int)row["PictureId"],
                            ParentId = (int)row["ParentId"],
                            DisplayOrder = (int)row["DisplayOrder"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            ShowOnLayout = (bool?)row["ShowOnLayout"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"]
                        };

                        CurItem.Translation = new CategoryTranslationEntity
                        {
                            CategoryId = CurItem.Id,
                            DescriptionTranslation = (row["Description"] == DBNull.Value) ? null : (string)row["Description"],
                            Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                        };

                        CurItem.Picture = new PictureEntity
                        {
                            Main = (bool)row["Main"],
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"]
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

        public int? Insert(CategoryEntity itm)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("CategoryInsert", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("PictureId", itm.PictureId);
                cmd.Parameters.AddWithValue("ParentId", itm.ParentId);
                cmd.Parameters.AddWithValue("Published", itm.Published);
                cmd.Parameters.AddWithValue("DisplayOrder", itm.DisplayOrder);
                cmd.Parameters.AddWithValue("ShowOnLayout", itm.ShowOnLayout);
                cmd.Parameters.AddWithValue("ShowOnHomePage", itm.ShowOnHomePage);

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


        public int? InsertTranslation(CategoryTranslationEntity itm)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("CategoryTranslationInsert", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CategoryId", itm.CategoryId);
                cmd.Parameters.AddWithValue("Language", itm.Language);
                cmd.Parameters.AddWithValue("Description", itm.DescriptionTranslation);
                cmd.Parameters.AddWithValue("Name", itm.Name);
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

        public List<CategoryEntity> GetCategoriesTreeByParentId(int ParentId = 0, string lang = "hy")
        {
            List<CategoryEntity> list = new List<CategoryEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetCategoriesTreeByParentId", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ParentId", ParentId);
                cmd.Parameters.AddWithValue("Status", true);
                cmd.Parameters.AddWithValue("Published", true);
                cmd.Parameters.AddWithValue("Language", lang);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new CategoryEntity
                        {
                            Id = (int)row["Id"],
                            PictureId = (int)row["PictureId"],
                            ParentId = (int)row["ParentId"],
                            DisplayOrder = (int)row["DisplayOrder"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"]
                        };

                        CurItem.Translation = new CategoryTranslationEntity
                        {
                            CategoryId = CurItem.Id,
                            DescriptionTranslation = (row["Description"] == DBNull.Value) ? null : (string)row["Description"],
                            Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                        };

                        CurItem.Picture = new PictureEntity
                        {
                            Main = (bool)row["Main"],
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"]
                        };

                        list.Add(CurItem);
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
        public List<CategoryEntity> GetCategoriesByParentId(int ParentId = 0)
        {
            List<CategoryEntity> list = new List<CategoryEntity>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetCategoriesByParentId", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ParentId", ParentId);
                cmd.Parameters.AddWithValue("Language", "hy");

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var CurItem = new CategoryEntity
                        {
                            Id = (int)row["Id"],
                            PictureId = (int)row["PictureId"],
                            ParentId = (int)row["ParentId"],
                            DisplayOrder = (int)row["DisplayOrder"],
                            ShowOnHomePage = (bool)row["ShowOnHomePage"],
                            Published = (bool)row["Published"],
                            CreationDate = (DateTime)row["CreationDate"]
                        };

                        CurItem.Translation = new CategoryTranslationEntity
                        {
                            CategoryId = CurItem.Id,
                            DescriptionTranslation = (row["Description"] == DBNull.Value) ? null : (string)row["Description"],
                            Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                        };

                        CurItem.Picture = new PictureEntity
                        {
                            Main = (bool)row["Main"],
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"]
                        };

                        list.Add(CurItem);
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
    }
}
