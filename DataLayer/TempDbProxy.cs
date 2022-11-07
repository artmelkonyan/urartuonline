using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DataLayer
{
    public class TempDbProxy : OldDbProxy
    {
        public List<ProductEntity> GetProductDescs()
        {
            List<ProductEntity> list = new List<ProductEntity>();

            this.Open();

            string queryString = "select Product.Sku, Product.ShortDescription, Product.FullDescription from Product";

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
                        if (row["Sku"] != DBNull.Value)
                        {

                            ProductEntity entity = new ProductEntity();
                            entity.Sku = (string)row["Sku"];

                            entity.Translation = new ProductTranslationEntity
                            {
                                ShortDescriptionTranslation = (row["ShortDescription"] == DBNull.Value) ? null : (string)row["ShortDescription"],
                                FullDescriptionTranslation = (row["FullDescription"] == DBNull.Value) ? null : (string)row["FullDescription"]
                            };

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
        public List<ProductCategoryMappingEntity> GetProductsToCategories()
        {
            List<ProductCategoryMappingEntity> list = new List<ProductCategoryMappingEntity>();

            this.Open();

            string queryString = "select *"
                + " From Product_Category_Mapping"
                + " INNER JOIN (select * from Product) pr ON pr.Id = Product_Category_Mapping.ProductId";

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
                        if (row["Sku"] != DBNull.Value)
                        {

                            ProductCategoryMappingEntity entity = new ProductCategoryMappingEntity();
                            entity.ProductOldid = (int)row["ProductId"];
                            entity.CategoryOldId = (int)row["CategoryId"];
                            entity.Sku = (string)row["Sku"];
                            list.Add(entity);
                        }
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
        public List<ProductPictureEntity> SelectPictureListBySku(string p_Sku)
        {
            List<ProductPictureEntity> list = new List<ProductPictureEntity>();
            this.Open();

            string queryString = "select p.Id, s.PictureId as PicId, pic.SeoFilename as PicName from dbo.Product p"
    + " INNER JOIN (Select * from dbo.Product_Picture_Mapping) s"
		+ " ON s.ProductId = p.Id"
			+ " INNER JOIN (Select * from Picture) pic"
				+ " ON pic.Id = s.PictureId"
                    + " Where p.Sku = @sku";
            
            SqlCommand cmd = new SqlCommand(queryString, this.mConnection);
            cmd.Parameters.AddWithValue("@sku", p_Sku);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductPictureEntity entity = new ProductPictureEntity();
                    entity.PicId = ((Int32)reader.GetValue(reader.GetOrdinal("PicId"))).ToString();
                    entity.PicName = (string)reader.GetValue(reader.GetOrdinal("PicName"));
                    list.Add(entity);
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.Close();
            }
            return list;
        }


        public List<CategoryEntity> GetOldCategories()
        {
            List<CategoryEntity> list = new List<CategoryEntity>();
            this.Open();

            string queryString = "select c.*, pic.SeoFilename from dbo.Category c"
                + " INNER JOIN (Select * from Picture Where Picture.SeoFilename != '') pic"
                 + " ON pic.Id = c.PictureId"
                     + " where c.Deleted = 0 AND c.Published = 1";

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
                        entity.PictureId = (int)row["PictureId"];
                        entity.OldId = (int)row["Id"];
                        entity.ParentId = (int)row["ParentCategoryId"];
                        entity.DisplayOrder = (int)row["DisplayOrder"];
                        entity.ShowOnHomePage = (bool)row["ShowOnHomePage"];
                        entity.Published = (bool)row["Published"];

                        entity.CategoryPicture = new CategoryPictureEntity();
                        entity.CategoryPicture.PicId = entity.PictureId;
                        entity.CategoryPicture.PicName = (string)row["SeoFilename"];

                        entity.Translation = new CategoryTranslationEntity
                        {
                            DescriptionTranslation = (row["Description"] == DBNull.Value) ? null : (string)row["Description"],
                            Name = (row["Name"] == DBNull.Value) ? null : (string)row["Name"]
                        };

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
