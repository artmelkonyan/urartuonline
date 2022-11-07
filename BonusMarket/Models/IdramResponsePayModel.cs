using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusMarket.Models
{
    public class IdramResponsePayModel
    {
        public string EDP_PRECHECK { get; set; }
        public string EDP_BILL_NO { get; set; }
        public string EDP_REC_ACCOUNT { get; set; } // oduble to string changed
        public string EDP_PAYER_ACCOUNT { get; set; }
        public string EDP_AMOUNT { get; set; }
        public string EDP_TRANS_ID { get; set; }
        public string EDP_TRANS_DATE { get; set; }
        public string EDP_CHECKSUM { get; set; }
        public string test { get; set; }
    }
    public class Test
    {
        public string RequestUri { get; set; }
    }
}

/*
    EDP_BILL_NO This field contains bill ID according to the
merchant’s accounting system
    EDP_REC_ACCOUNT Merchant IdramID (in Idram system) to
which customer made the payment
    EDP_PAYER_ACCOUNT Customer IdramID (in Idram system) from
which customer made the payment
    EDP_AMOUNT
(format-0.00)
Amount that must be paid by the
customer. Fraction must be separated by
period (dot)
    EDP_TRANS_ID
format - char(14)
Payment transaction ID in Idram system. A
unique number in Idram system.
    EDP_TRANS_DATE
format -dd/mm/yyyy
Transaction date
    EDP_CHECKSUM
Payment confirmation message
checksum, which is used to check integrity
of the received data and unambiguous
identification of the sende
 
 */