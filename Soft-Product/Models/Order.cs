using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Models
{
    public class Order
    {
       [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string  Status { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsDelivered { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

       
    }
}
