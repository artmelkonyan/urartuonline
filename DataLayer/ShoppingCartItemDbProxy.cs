using Models.EntityModels;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ShoppingCartItemDbProxy : BaseDbProxy
    {
        public ShoppingCartItem Insert(ShoppingCartItem item)
        {
            this.Open();

            SqlCommand cmd = new SqlCommand("InsertShoppingCartItem", this.mConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("ClientId", item.ClientId);
                cmd.Parameters.AddWithValue("Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("ShoppingCartItemId", item.ShoppingCartItemTypeId);
                cmd.Parameters.AddWithValue("CreatedOn", item.CreatedOn);

                DataTable dt = new DataTable();

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

                    item.Id = decimal.ToInt32(decId);
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

            return item;
        }

        public List<ShoppingCartItem> GetAll(int clientId, ShoppingCartItemType shoppingCartItemType)
        {
            var list = new List<ShoppingCartItem>();

            this.Open();

            SqlCommand cmd = new SqlCommand("GetShoppingCartItems", this.mConnection);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("ClientId", clientId);
                cmd.Parameters.AddWithValue("ShoppingCartItemTypeId", (int)shoppingCartItemType);


                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);


                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var shoppingCartItem = new ShoppingCartItem
                        {
                            Id = (int)row["Id"],
                            ProductId = (int)row["ProductId"],
                            ClientId = (int)row["ClientId"],
                            Quantity = (int)row["Quantity"],
                            ShoppingCartItemTypeId = (int)row["ShoppingCartTypeId"],
                            CreatedOn = (DateTime)row["CreatedOn"],
                            UpdatedOn = row["UpdatedOn"] == DBNull.Value?null: (DateTime?)row["UpdatedOn"]
                        };

                        list.Add(shoppingCartItem);
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
