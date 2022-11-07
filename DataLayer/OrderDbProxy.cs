using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderDbProxy : BaseDbProxy
    {

        public void DeleteOrderById(int id)
        {
            OrderEntity CurItem = new OrderEntity();

            this.Open();

            SqlCommand cmd = new SqlCommand("DeleteOrderById", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("OrderId", id);
                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

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
        public OrderEntity GetOrderById(int id)
        {
            OrderEntity CurItem = new OrderEntity();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetOrderById", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("OrderId", id);


                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["UserId"] == DBNull.Value)
                        {
                            CurItem.Address = (dt.Rows[0]["Address"] == DBNull.Value) ? null : (string)dt.Rows[0]["Address"];
                            CurItem.FirstName = (dt.Rows[0]["FirstName"] == DBNull.Value) ? null : (string)dt.Rows[0]["FirstName"];
                            CurItem.LastName = (dt.Rows[0]["LastName"] == DBNull.Value) ? null : (string)dt.Rows[0]["LastName"];
                            CurItem.Phone = (dt.Rows[0]["Phone"] == DBNull.Value) ? null : (string)dt.Rows[0]["Phone"];
                            CurItem.Email = (dt.Rows[0]["Email"] == DBNull.Value) ? null : (string)dt.Rows[0]["Email"];
                        }
                        else
                        {
                            CurItem.Address = (dt.Rows[0]["UserAddress"] == DBNull.Value) ? null : (string)dt.Rows[0]["UserAddress"];
                            CurItem.FirstName = (dt.Rows[0]["UserFirstName"] == DBNull.Value) ? null : (string)dt.Rows[0]["UserFirstName"];
                            CurItem.LastName = (dt.Rows[0]["UserLastName"] == DBNull.Value) ? null : (string)dt.Rows[0]["UserLastName"];
                            CurItem.Phone = (dt.Rows[0]["UserPhone"] == DBNull.Value) ? null : (string)dt.Rows[0]["UserPhone"];
                            CurItem.Email = (dt.Rows[0]["Useremail"] == DBNull.Value) ? null : (string)dt.Rows[0]["Useremail"];
                        }
                    }
                    CurItem.Id = (int)dt.Rows[0]["Id"];
                    CurItem.CreationDate = (DateTime)dt.Rows[0]["CreateDate"];
                    CurItem.PaymentMethod = (bool)dt.Rows[0]["Paymentmetod"];
                    CurItem.TotalMoney = Convert.ToInt32((decimal)dt.Rows[0]["TotalMoney"]);
                    CurItem.ShipmentStatus = (byte)dt.Rows[0]["ShippingStatus"];
                    CurItem.BankOrderId = (dt.Rows[0]["BankOrderId"] == DBNull.Value) ? null : (int?)dt.Rows[0]["BankOrderId"];
                    CurItem.OrderComment = (dt.Rows[0]["OrderComment"] == DBNull.Value) ? null : (string)dt.Rows[0]["OrderComment"];
                    List<ProductEntity> OrdrProducts = new List<ProductEntity>();
                    foreach (DataRow row in dt.Rows)
                    {
                        OrdrProducts.Add(new ProductEntity
                        {
                            Id = (int)row["OrderProductId"],
                            Price = (decimal?)row["OrderProductPrice"],
                            Sku = row["OrderProductSku"] == DBNull.Value ? "" : (string)row["OrderProductSku"],
                            Translation = new ProductTranslationEntity()
                            {
                                NameTranslation = (row["Name"] == DBNull.Value) ? null : (string)row["Name"],
                            },
                            Count = (int)row["OrderProductsCount"]
                        });
                    }
                    CurItem.OrderedProducts = OrdrProducts;
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


    }
}
