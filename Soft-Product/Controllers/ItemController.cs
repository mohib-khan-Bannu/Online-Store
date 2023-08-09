using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soft_Product.Models;
using Soft_Product.Models.ItemsViewModa_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItemController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {

            //var getItems = context.Items.ToList();

            //var plumbingCategoryCount = getItems.Where(p => p.CategoryId == 1).Count();
            //var electronicCategoryCount = getItems.Where(p => p.CategoryId == 2).Count();




            var getCategoryList = GetCategoryListForDropDown();
            ViewBag.CategoryList = getCategoryList;

            return View();
        }
        public JsonResult ItemList()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";

            var list = context.Items.Include(c => c.Category).Select(a => new ItemViewModal
            {
                ItemId = a.ItemId,
                Name = a.Name,
                ProfileImage = domain + "/" + a.Image,
                Price = a.Price,
                Quantity = a.Quantity,
                ItemInStock = a.ItemInStock,
                ExpireDate = a.ExpireDate.ToString("MM/dd/yyyy"),
                CategoryId = a.CategoryId,
                CategoryName = a.Category != null ? a.Category.Name : "N/A",
            }).ToList();
            return new JsonResult(list);
        }
        
        public async Task<JsonResult> AddItems(AddViewModalItem item)
        {
            IFormFile file = item.ProfileImage;

            Random random = new Random();
            string filename = "profilePicture" + random.Next(0, 999999999) + Path.GetExtension(file.FileName);
            using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                await file.CopyToAsync(output);

            var profileImagePath = Path.Combine("UploadItems", "ItemsProfile", filename).Replace("\\", "/");

            var data = new Item()
            {
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity,
                ItemInStock = item.ItemInStock,
                ExpireDate = item.ExpireDate,
                CategoryId = item.CategoryId,
                Image = profileImagePath,

            };

            context.Items.Add(data);
            context.SaveChanges();
            return new JsonResult("Data is Saved");
        }
        public JsonResult Edit(int id)
        {
            var requset = _httpContextAccessor.HttpContext.Request;
            var domain = $"{requset.Scheme}://{requset.Host}";
            var data = context.Items.Where(s => s.ItemId == id).Select(c => new EditItemViewModal
            {
                ItemId = c.ItemId,
                Name = c.Name,
                Price = c.Price,
                Quantity = c.Quantity,
                ItemInStock = c.ItemInStock,
                ExpireDate = c.ExpireDate,
                CategoryId = c.CategoryId,
                ProfileImage = domain + "/" + c.Image,

            }).SingleOrDefault();
            return new JsonResult(data);
        }
        public async Task<JsonResult> UpdateItem(UpdateItemViewModal updateitem)
        {
            var data = context.Items.Where(s => s.ItemId == updateitem.ItemId).SingleOrDefault();
            if(data != null)
            {
                var profileImagePath = string.Empty;
            
            if (updateitem.ProfileImage != null)
                {
                    IFormFile file = updateitem.ProfileImage;

                    Random random = new Random();
                    string filename = "profilePicture" + random.Next(0, 999999999) + Path.GetExtension(file.FileName);
                    using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                        await file.CopyToAsync(output);

                     profileImagePath = Path.Combine("UploadItems", "ItemsProfile", filename).Replace("\\", "/");
                }
                data.Name = updateitem.Name;
                data.Price = updateitem.Price;
                data.Quantity = updateitem.Quantity;
                data.ItemInStock = updateitem.ItemInStock;
                data.CategoryId = updateitem.CategoryId;
                data.Image = updateitem.ProfileImage != null ? profileImagePath : data.Image;

                context.Items.Update(data);
                context.SaveChanges();
                return new JsonResult("Data can be Update");
            }else
            {

            return new JsonResult("Data CAnt be update");
            }
        }
        public JsonResult Delete(int id)
        {
            if (id >  0)
            {
                var data = context.Items.Where(s => s.ItemId == id).SingleOrDefault();
                context.Items.Remove(data);
                context.SaveChanges();
                return new JsonResult("Data Can be Deleted");
            }
            else
            {
                return new JsonResult("Data cant Delete");
            }
        }
        private string GetPathAndFilename(string filename)
        {
            return Path.Combine(this._webHostEnvironment.WebRootPath, "UploadItems", "ItemsProfile", filename).Replace("\\", "/");
        }

        private List<NameOrValueDto> GetCategoryListForDropDown()
        {
            var getCategoryList = context.categories.Select(c => new NameOrValueDto()
            {
                Value = c.CategoryId,
                Name = c.Name,
            }).ToList();

            return getCategoryList;
        }
    }
}
