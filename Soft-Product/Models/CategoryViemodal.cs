using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Models
{
    public class CategoryViemodal
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string ProfileImage { get; set; }

       
    }
}
