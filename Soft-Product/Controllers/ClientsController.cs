using Microsoft.AspNetCore.Mvc;
using Soft_Product.Models;
using Soft_Product.Models.Extenstion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext context;

        public ClientsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var categorylist = context.categories.ToList();
            HttpContext.Session.SetComplexData("categoryList", categorylist);

            return View();
        }
        public IActionResult getproductbycategoryid(int categoryId)
        {
            var produtlist = context.Items.Where(s => s.CategoryId == categoryId).ToList();
           
            return View(produtlist);
        }
      
       
        

    }
}
