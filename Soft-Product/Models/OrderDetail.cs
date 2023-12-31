﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Models
{
    public class OrderDetail
    {
        [Key]
        public int Oderdetailid  { get; set; }
        public int OrderId { get; set; }
        
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }

        public virtual Order order { get; set; }
        public virtual Item item { get; set; }
    }
}
