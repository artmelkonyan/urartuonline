using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class OrderEntity : BaseEntity
    {
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        public string Email { get; set; } = null;
        public string Address { get; set; } = null;
        public string Phone { get; set; } = null;
        public string OrderComment { get; set; } = null;
        public int? BankOrderId { get; set; } = null;
        public List<ProductEntity> OrderedProducts { get; set; } = null;
        public bool? PaymentMethod { get; set; } = null;
        public bool ShipmentMetod { get; set; } 
        public byte ShipmentStatus { get; set; }
        public int TotalMoney { get; set; }
    }
    [Serializable]
    public class OrderEntityViewModel : BaseEntityViewModel
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; } 
        public string UserName { get; set; }
        public List<ProductEntity> OrderedProducts { get; set; }
        public bool PaymentMethod { get; set; }
        public bool ShipmentMetod { get; set; }
        public string OrderComment { get; set; }
        public byte ShipmentStatus { get; set; }
        public int? BankOrderId { get; set; }
        public int TotalMoney { get; set; }

    }
}
