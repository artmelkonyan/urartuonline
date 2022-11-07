using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class IdramDbProxy:BaseDbProxy
    {
        public int? Insert(IdramPay itm)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("IdramPaymentsInsert", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("OrderId", itm.OrderId);
                cmd.Parameters.AddWithValue("BuildId", itm.BillId);
                cmd.Parameters.AddWithValue("Amount", itm.Amount);
                cmd.Parameters.AddWithValue("IsPay", itm.IsPay);
                cmd.Parameters.AddWithValue("CreateDate", itm.CreationDate);

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
                throw ex;
            }
            finally
            {
                this.Close();

            }

            return id;
        }


        public IdramPay GetidramPayByOrderId(int OrderId = 0)
        {

            this.Open();
            IdramPay CurItem=null;

            SqlCommand cmd = new SqlCommand("GetIdramPayByOrderId", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("OrderId", OrderId);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                         CurItem = new IdramPay
                        {
                            Id = (int)row["Id"],
                            OrderId = (int)row["OrderId"],
                            BillId = row["BuildId"].ToString(),
                            Amount = (double)row["DisplayOrder"],
                            IsPay = (bool)row["IsPay"],
                            CreationDate = (DateTime)row["CreateDate"]
                        };


                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                throw ex;
            }
            finally
            {
                this.Close();
            }
            return CurItem;
        }

        public IdramPay GetidramPayByBillId(string buildId )
        {

            this.Open();
            IdramPay CurItem = null;

            SqlCommand cmd = new SqlCommand("GetIdramPayByBillId", this.mConnection);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BillId", buildId);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        CurItem = new IdramPay
                        {
                            Id = (int)row["Id"],
                            OrderId = (int)row["OrderId"],
                            BillId = row["BuildId"].ToString(),
                            Amount = (double)row["Amount"],
                            IsPay = (bool)row["IsPay"],
                            CreationDate = (DateTime)row["CreateDate"]
                        };


                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                throw ex;
            }
            finally
            {
                this.Close();
            }
            return CurItem;
        }

        public bool UpdateIdramPay(IdramPay entity)
        {
            bool status = true;


            this.Open();

            SqlCommand cmd = new SqlCommand("UpdateIdramPay", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Id", entity.Id);
                cmd.Parameters.AddWithValue("OrderId", entity.OrderId);
                cmd.Parameters.AddWithValue("IsPay", entity.IsPay);
                cmd.Parameters.AddWithValue("Amount", entity.Amount);
                cmd.Parameters.AddWithValue("BuildId", entity.BillId);

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
                throw ex;
                return status;
            }
            finally
            {
                this.Close();
            }

            return status;
        }
    }
}
