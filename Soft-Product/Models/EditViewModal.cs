using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Models
{
    public class EditViewModal
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string ProfileImage { get; set; }
    }
}
