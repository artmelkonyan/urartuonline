using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class OldDbProxy
    {

        private string mConnectionString;
        public SqlConnection mConnection;
        public OldDbProxy()
        {
            mConnectionString = "Data Source=HARUTYUNKAM2CFF;Initial Catalog=BonusM;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=4815162342";
        }

        public void Open()
        {
            mConnection = new SqlConnection(mConnectionString);

            mConnection.Open();
        }

        public void Open(string ConnectionString)
        {
            mConnection = new SqlConnection(ConnectionString);

            mConnection.Open();
        }

        public void Close()
        {
            mConnection.Close();
        }
    }
}