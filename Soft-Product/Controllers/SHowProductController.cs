using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soft_Product.Models;
using Soft_Product.Models.ItemsViewModa_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Controllers
{
    public class SHowProductController : Controller
    {
        private readonly ApplicationDbContext context;

        public SHowProductController(ApplicationDbContext context )
        {
            this.context = context;
        }
        public IActionResult  Index()
        {
            var list = context.Items.ToList();
            return View(list);
           
        }
    }
}
