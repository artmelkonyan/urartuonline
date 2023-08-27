using Models;
using Models.EntityModels;
using Models.EntityModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class BannerDbProxy : BaseDbProxy
    {
        public int? Insert(BannerModel model)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("BannerInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("PictureId", model.PictureId);
                cmd.Parameters.AddWithValue("Show", model.Show);
                cmd.Parameters.AddWithValue("OrderId", model.OrderId);
                cmd.Parameters.AddWithValue("Link", model.Link);

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

        public int? InsertAdvertismentBanner(BannerModel model)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("AdvertismentBannerInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("PictureId", model.PictureId);
                cmd.Parameters.AddWithValue("Show", model.Show);
                cmd.Parameters.AddWithValue("OrderId", model.OrderId);
                cmd.Parameters.AddWithValue("Link", model.Link);

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

        public BannerModel GetById(int id)
        {
            BannerModel entity = new BannerModel();


            this.Open();

            SqlCommand cmd = new SqlCommand("GetBannerById", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", id);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        entity = new BannerModel
                        {
                            Id = (int)row["Id"],
                            PictureId = (int)row["PictureId"],
                            Link = (string)(row["Link"] == DBNull.Value ? "" : row["Link"]),
                            OrderId = (int)row["OrderId"],
                            Show = (bool)row["Show"]
                        };

                        entity.Picture = new PictureEntityViewModel
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
        public bool UpdateBanner(BannerModel model)
        {
            bool status = true;

            this.Open();

            SqlCommand cmd = new SqlCommand("UpdateBanner", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", model.Id);
                cmd.Parameters.AddWithValue("PictureId", model.PictureId);
                cmd.Parameters.AddWithValue("Show", model.Show);
                cmd.Parameters.AddWithValue("OrderId", model.OrderId);
                cmd.Parameters.AddWithValue("Link", model.Link);

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

        public bool UpdateAdvertismentBanner(BannerModel model)
        {
            bool status = true;

            this.Open();

            SqlCommand cmd = new SqlCommand("UpdateAdvertismentBanner", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", model.Id);
                cmd.Parameters.AddWithValue("PictureId", model.PictureId);
                cmd.Parameters.AddWithValue("Show", model.Show);
                cmd.Parameters.AddWithValue("OrderId", model.OrderId);
                cmd.Parameters.AddWithValue("Link", model.Link);

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

        public List<Banner> GetAllBanners()
        {
            List<Banner> list = new List<Banner>();
            this.Open();

            string queryString = "select b.*,p.FullPath from Banner b join Pictures p on b.PictureId=p.Id";

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
                        Banner entity = new Banner();
                        entity.Id = (int)row["Id"];
                        entity.OrderId = (int)row["OrderId"];
                        entity.PictureId = (int)row["PictureId"];
                        entity.Link = (string)(row["Link"] == DBNull.Value ? "" : row["Link"]);
                        entity.Show = (bool)row["Show"];
                        entity.Picture = new PictureEntityViewModel();
                        entity.Picture.Id = (int)row["PictureId"];
                        entity.Picture.FullPath = (string)row["FullPath"];
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


        public List<AdvertismentBanner> GetAllAdvertismentBanners()
        {
            List<AdvertismentBanner> list = new List<AdvertismentBanner>();
            this.Open();

            string queryString = "select b.*,p.FullPath from AdvertismentBanner b join Pictures p on b.PictureId=p.Id";

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
                        AdvertismentBanner entity = new AdvertismentBanner();
                        entity.Id = (int)row["Id"];
                        entity.OrderId = (int)row["OrderId"];
                        entity.PictureId = (int)row["PictureId"];
                        entity.Link = (string)(row["Link"] == DBNull.Value ? "" : row["Link"]);
                        entity.Show = (bool)row["Show"];
                        entity.Picture = new PictureEntityViewModel();
                        entity.Picture.Id = (int)row["PictureId"];
                        entity.Picture.FullPath = (string)row["FullPath"];
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

    }
}
