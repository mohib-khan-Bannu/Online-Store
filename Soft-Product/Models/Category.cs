using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string ProfilePicture { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
