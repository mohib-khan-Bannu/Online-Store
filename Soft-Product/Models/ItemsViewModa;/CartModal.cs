using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Models.ItemsViewModa_
{
    public class CartModal
    {
        public int ItemId { get; set; }
        public long Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
    }
}
