using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class PictureDbProxy : BaseDbProxy
    {
        public bool RemoveImage(int id)
        {
            bool status = true;

            this.Open();

            string queryString = "UPDATE Pictures SET Status = 0 WHERE Id = " + id.ToString();

            SqlCommand cmd = new SqlCommand(queryString, this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt == null)
                {
                    status = false;
                }
                cmd.Dispose();
            }
            catch(Exception ex)
            {
                cmd.Dispose();
            }
            finally
            {
                this.Close();

            }
            return status;
        }
        public PictureEntityViewModel GetPictureById(int id)
        {
            PictureEntityViewModel model = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("GetPictureById", this.mConnection);
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
                        model = new PictureEntityViewModel()
                        {
                            Id = (int)row["ID"],
                            Main = (bool)row["Main"],
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            Status = (bool)row["Status"]
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
        public bool RemoveProductPicture(int id)
        {
            bool status = true;

            this.Open();

            string queryString = "UPDATE Product_To_Picture SET Status = 0 WHERE PictureId = " + id.ToString();

            SqlCommand cmd = new SqlCommand(queryString, this.mConnection);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

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
        public int? Insert(PictureEntity itm)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("PictureInsert", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("RealPath", itm.RealPath);
                cmd.Parameters.AddWithValue("RealName", itm.RealName);
                cmd.Parameters.AddWithValue("FullPath", itm.FullPath);
                cmd.Parameters.AddWithValue("SeoName", itm.SeoName);
                cmd.Parameters.AddWithValue("Main", itm.Main);
                cmd.Parameters.AddWithValue("CreationDate", itm.CreationDate);

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

        public int? InsertProductMapping(ProductPictureMapping itm)
        {

            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("ProductPictureMappingInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ProductId", itm.ProductId);
                cmd.Parameters.AddWithValue("PictureId", itm.PictureId);

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

        public List<PictureEntity> GetProductPictures(int ?id)
        {
            List<PictureEntity> list = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("GetProductPictures", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ProductId", id);
                cmd.Parameters.AddWithValue("Status", true);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    list = new List<PictureEntity>();
                    foreach (DataRow row in dt.Rows)
                    {
                        PictureEntity item = new PictureEntity
                        {
                            Id = (int)row["ID"],
                            Main = (bool)row["Main"],
                            FullPath = (row["FullPath"] == DBNull.Value) ? null : (string)row["FullPath"],
                            SeoName = (row["SeoName"] == DBNull.Value) ? null : (string)row["SeoName"],
                            Status = (bool)row["Status"]
                        };

                        list.Add(item);
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex) {
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
