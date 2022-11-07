using Models;
using Models.EntityModels;
using Models.EntityModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class BrandDbProxy : BaseDbProxy
    {
        public int GetBrandCountBySearch(string text, string lang = "hy")
        {
            int count = 0;
            this.Open();
            SqlCommand cmd = new SqlCommand("GetBrandCountBySearch", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                DataTable dt = new DataTable();

                cmd.Parameters.AddWithValue("Name", text);
                cmd.Parameters.AddWithValue("Language", lang);
                cmd.Parameters.AddWithValue("CountBrand", count);

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        count = (int)row["CountBrands"];
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return 0;
            }

            finally
            {
                this.Close();
            }

            return count;
        }
       public List<BrandModel> GetBrandsBySearch(string name, string lang)
        {
            List<BrandModel> p_list = new List<BrandModel>();
            this.Open();
            SqlCommand cmd = new SqlCommand("GetBrandsBySearch", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Search", name);
                cmd.Parameters.AddWithValue("Language", lang);

                //cmd.Parameters.AddWithValue("PageSize", pageSize);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        p_list.Add(new BrandModel
                        {
                            Show = (bool)row["Show"],
                            Translate = new BrandTranslate
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Id = (int)row["Id"],
                            OrderId = (row["OrderId"] == DBNull.Value) ? 0 : (int)row["OrderId"],
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

            return p_list;
        }
        public int GetBrandsCount()
        {
            int count = 0;
            this.Open();
            SqlCommand cmd = new SqlCommand("GetBrandCount", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                cmd.Parameters.AddWithValue("CountBrand", count);
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        count = (int)row["CountBrands"];
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                return 0;
            }

            finally
            {
                this.Close();
            }

            return count;
        }
        public List<BrandModel> GetOnHomePage(string lang = "hy")
        {
            List<BrandModel> c_list = new List<BrandModel>();
            this.Open();

            SqlCommand cmd = new SqlCommand("GetBrandOnHomePage", this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("LanguageId", lang);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        c_list.Add(new BrandModel
                        {
                            Translate = new BrandTranslate()
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Picture = new PictureEntityViewModel()
                            {
                                FullPath = (row["FullPath"] == DBNull.Value) ? "" : (string)row["FullPath"]
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
            return c_list.OrderBy(x=>x.OrderId).ToList();

        }
        public List<BrandModel> GetBrandsByCategoryId(int categoryId, string lang = "hy")
        {
            List<BrandModel> c_list = new List<BrandModel>();
            this.Open();

            SqlCommand cmd = new SqlCommand("GetFilterBrandsByCategoryId", this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CategoryId", categoryId);
                cmd.Parameters.AddWithValue("Published", true);
                cmd.Parameters.AddWithValue("LanguageId", lang);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var id = (int)row["Id"];
                        if (c_list.Any(x => x.Id == id))
                            continue;
                        c_list.Add(new BrandModel
                        {
                            Translate = new BrandTranslate()
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
        public List<BrandModel> GetAll(int page, int pageSize, string searchText, string lang = "hy")
        {

            List<BrandModel> p_list = new List<BrandModel>();
            this.Open();
            SqlCommand cmd = new SqlCommand("GetBrandsSearch", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                if (page < 1)
                {
                    page = 1;
                }
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Name", searchText);
                cmd.Parameters.AddWithValue("CurrentPage", page);
                if (pageSize > 1)
                {
                    cmd.Parameters.AddWithValue("PageSize", pageSize);
                }
                //cmd.Parameters.AddWithValue("PageSize", pageSize);
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        p_list.Add(new BrandModel
                        {
                            Show = (bool)row["Show"],
                            Translate = new BrandTranslate
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Id = (int)row["Id"],
                            OrderId = (row["OrderId"] == DBNull.Value) ? 0 : (int)row["OrderId"],
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

            return p_list;
        }
        public List<BrandModel> GetAll(int page, int pageSize, string lang = "hy")
        {
            List<BrandModel> p_list = new List<BrandModel>();
            this.Open();
            SqlCommand cmd = new SqlCommand("GetDashboardsBrands", this.mConnection);
            try
            {


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                if (page < 1)
                {
                    page = 1;
                }
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CurrentPage", page);
                if (pageSize > 1)
                {
                    cmd.Parameters.AddWithValue("PageSize", pageSize);
                }
                //cmd.Parameters.AddWithValue("PageSize", pageSize);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        p_list.Add(new BrandModel
                        {
                            Show = (bool)row["Show"],
                            Translate = new BrandTranslate
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Id = (int)row["Id"],
                            OrderId = (row["OrderId"] == DBNull.Value) ? 0 : (int)row["OrderId"],
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

            return p_list;
        }
        public int? AddBanner(BrandAdminModel model)
        {
            int? id = null;

            this.Open();
            SqlCommand cmd = new SqlCommand("BrandInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("PictureId", model.PictureId);
                cmd.Parameters.AddWithValue("Show", model.Show);
                cmd.Parameters.AddWithValue("OrderId", model.OrderId);
                cmd.Parameters.AddWithValue("Status", true);

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
        public void EditBanner(BrandAdminModel model)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand("UpdateBrand", this.mConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                cmd.Parameters.AddWithValue("Id", model.Id);
                cmd.Parameters.AddWithValue("PictureId", model.PictureId);
                cmd.Parameters.AddWithValue("Show", model.Show);
                cmd.Parameters.AddWithValue("OrderId", model.OrderId);
                //cmd.Parameters.AddWithValue("Status", true);

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
        public BrandAdminModel GetBrandDashboardById(int id)
        {

            BrandAdminModel model = new BrandAdminModel();
            this.Open();

            SqlCommand cmd = new SqlCommand("GetBrandDashboardById", this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", id);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    var trans = new List<BrandTranslate>();
                    foreach (DataRow row in dt.Rows)
                    {
                        model = new BrandAdminModel()
                        {

                            Picture = new PictureEntityViewModel()
                            {
                                FullPath = (row["FullPath"] == DBNull.Value) ? "" : (string)row["FullPath"]
                            },
                            OrderId = (int)row["OrderId"],
                            PictureId = (int)row["PictureId"],
                            Show = (bool)row["Show"],
                            Id = (int)row["Id"],
                        };
                        trans.Add(new BrandTranslate()
                        {
                            Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            Language = (string)row["Language"],
                            Id = (int)row["TranslationId"]
                        });
                    }
                    model.BrandTranslates = trans;
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
            return model;
        }
        public List<BrandModel> GetBrands(string lang = "hy")
        {

            List<BrandModel> c_list = new List<BrandModel>();
            this.Open();

            SqlCommand cmd = new SqlCommand("GetBrands", this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("LanguageId", lang);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        c_list.Add(new BrandModel
                        {
                            Translate = new BrandTranslate()
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Picture = new PictureEntityViewModel()
                            {
                                FullPath = (row["FullPath"] == DBNull.Value) ? "" : (string)row["FullPath"]
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

        public BrandModel GetBrandById(int id, string lang = "hy")
        {

            BrandModel model = new BrandModel();
            this.Open();

            SqlCommand cmd = new SqlCommand("GetBrandById", this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("LanguageId", lang);
                cmd.Parameters.AddWithValue("Id", id);

                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        model = new BrandModel()
                        {
                            Translate = new BrandTranslate()
                            {
                                Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Picture = new PictureEntityViewModel()
                            {
                                FullPath = (row["FullPath"] == DBNull.Value) ? "" : (string)row["FullPath"]
                            },
                            Id = (int)row["Id"],
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
            return model;
        }
        #region BrandTranslate
        public void AddBrandTranslate(BrandTranslate model)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand("BrandTranslateInsert", this.mConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                cmd.Parameters.AddWithValue("Name", model.Name);
                cmd.Parameters.AddWithValue("Language", model.Language);
                cmd.Parameters.AddWithValue("BrandId", model.BrandId);

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
        public void EditBrandTranslate(BrandTranslate model)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand("UpdateBrandTranslation", this.mConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.AddWithValue("Id", model.Id);
                cmd.Parameters.AddWithValue("Name", model.Name);
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
        #endregion
    }
}
