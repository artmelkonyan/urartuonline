using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDataLayer.Context
{
    public static class AppConnection
    {
        public static string ConnectionString
        {
            get
            {
                return "Data Source=213.136.89.64, 1433;Initial Catalog=BonusMarket;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=strongpass123.";
            }
        }
    }
}
