using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Models
{
    public class AddViewModalItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public IFormFile ProfileImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ItemSold { get; set; }
        public int ItemInStock { get; set; }
        public DateTime ExpireDate { get; set; }
        public int? CategoryId { get; set; }
        
    }
}
