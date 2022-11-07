using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataLayer
{
    public class BaseDbProxy
    {
        // TODO get connection string
        private string mConnectionString;
        public SqlConnection mConnection;
        public BaseDbProxy()
        {
            mConnectionString = "Data Source=213.136.89.64, 1433;Initial Catalog=BonusMarket;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=strongpass123.";
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
