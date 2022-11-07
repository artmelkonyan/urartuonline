using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class IdramPay : BaseEntity
    {
        public string BillId { get; set; }
        public bool? IsPay { get; set; }
        public double Amount { get; set; }
        public int OrderId { get; set; }
    }
}
