using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GeneralHelper
    {
        public static string GetPriceString(decimal Price)
        {
            string priceString = Price.ToString();
            return priceString.Substring(0, priceString.Length - 2);
        }
    }
}
