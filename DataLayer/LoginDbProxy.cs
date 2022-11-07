using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class LoginDbProxy : BaseDbProxy
    {
        public UserEntity SignIn(LoginEntity user)
        {

            UserEntity entity = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("LoginUser", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Email", user.UserName);
                cmd.Parameters.AddWithValue("PasswordHash", user.PassWord);

                DataTable dt = new DataTable();

                // Set the data adapter’s select command
                da.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        entity = new UserEntity
                        {
                            Id = (int)row["Id"],
                            Email = (string)row["Email"],
                            FirstName = (row["FirstName"] == DBNull.Value) ? null : (string)row["FirstName"],
                            LastName = (row["LastName"] == DBNull.Value) ? null : (string)row["LastName"],
                            Address = (row["Address"] == DBNull.Value) ? null : (string)row["Address"],
                            Role = (UserRole)(int)row["Role"],
                            CreationDate = (DateTime)row["CreationDate"]
                        };
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
            return entity;
        }

        public int? Register(UserEntity itm)
        {
            int? id = null;

            this.Open();

            SqlCommand cmd = new SqlCommand("UsersInsert", this.mConnection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Email", itm.Email);
                cmd.Parameters.AddWithValue("FirstName", itm.FirstName);
                cmd.Parameters.AddWithValue("LastName", itm.LastName);
                cmd.Parameters.AddWithValue("Address", itm.Address);
                cmd.Parameters.AddWithValue("PasswordHash", itm.PasswordHash);
                cmd.Parameters.AddWithValue("Role", (int)itm.Role);
                cmd.Parameters.AddWithValue("Phone", itm.Phone);

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
    }
}
