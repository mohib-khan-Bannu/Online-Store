using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soft_Product.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult CategoryList()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";

            var list = context.categories.Select(x => new CategoryViemodal
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                Date = x.Date.ToString("MM/dd/yyyy"),
                ProfileImage = domain + "/" + x.ProfilePicture,
            }).ToList();

            return new JsonResult(list);
        }
        [HttpPost]
        public async Task<JsonResult> AddCategory(AddViewmodalcs category)
        {
            IFormFile file = category.ProfileImage;

            Random random = new Random();
            string filename = "profilePicture" + random.Next(0, 999999999) + Path.GetExtension(file.FileName);
            using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                await file.CopyToAsync(output);

            var profileImagePath = Path.Combine("UploadImages", "ProfileImages", filename).Replace("\\", "/");

            var data = new Category()
            {
                Name = category.Name,
                Date = category.Date,
                ProfilePicture = profileImagePath

            };

            context.categories.Add(data);
            context.SaveChanges();
            return new JsonResult("Data is Saved");
        }
        public JsonResult Delete(int id)
        {
            try
            {

                if (id > 0)
                {
                    var data = context.categories.Where(s => s.CategoryId == id).FirstOrDefault();
                    context.categories.Remove(data);
                    context.SaveChanges();
                    return new JsonResult("Data Can be Deleted");
                }
                else
                {
                    return new JsonResult("Data Cant be Delete");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public JsonResult Edit(int id)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";

            var data = context.categories.Where(d => d.CategoryId == id).Select(x => new EditViewModal
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                Date = x.Date,
                ProfileImage = domain + "/" + x.ProfilePicture,
            }).SingleOrDefault();


            return new JsonResult(data);
        }

        public async Task<JsonResult> UpdateCategory(Updateviewmodal category)
        {


            var data = context.categories.Where(d => d.CategoryId == category.CategoryId).SingleOrDefault();
            if (data != null)
            {
                var profileImagePath = string.Empty;
                if (category.ProfileImage != null)
                {
                    IFormFile file = category.ProfileImage;

                    Random random = new Random();
                    string filename = "profilePicture" + random.Next(0, 999999999) + Path.GetExtension(file.FileName);
                    using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                        await file.CopyToAsync(output);
                    profileImagePath = Path.Combine("UploadImages", "ProfileImages", filename).Replace("\\", "/");

                }

                data.Name = category.Name;
                data.Date = category.Date;
                data.ProfilePicture = category.ProfileImage != null ? profileImagePath : data.ProfilePicture;


                context.categories.Update(data);
                context.SaveChanges();

                return new JsonResult("Record Updated");
            }
            else
            {
                return new JsonResult("Invalid request");
            }



        }

        private string GetPathAndFilename(string filename)
        {
            return Path.Combine(this._webHostEnvironment.WebRootPath, "UploadImages", "ProfileImages", filename).Replace("\\", "/");
        }
    }
}
