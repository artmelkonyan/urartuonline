using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusMarket.Models
{
    public class IdramRequestPayModel
    {
        public string EDP_LANGUAGE { get; set; }
        public string EDP_REC_ACCOUNT { get; set; }
        public string EDP_DESCRIPTION { get; set; }
        public double EDP_AMOUNT { get; set; }
        public string EDP_BILL_NO { get; set; }
    }
}
