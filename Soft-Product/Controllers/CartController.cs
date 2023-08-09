using Microsoft.AspNetCore.Mvc;
using Soft_Product.Models;
using Soft_Product.Models.Extenstion;
using Soft_Product.Models.ItemsViewModa_;
using Soft_Product.Session.LearnASPNETCoreMVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Controllers
{

    public class CartController : Controller
    {
        List<CartModal> ListcartModals;

        private readonly ApplicationDbContext context;

        public CartController(ApplicationDbContext context)
        {
            this.context = context;
            ListcartModals = new List<CartModal>();
        }

        [HttpPost]
        public JsonResult Index(int ItemId)
        {

            CartModal objcartModal = new CartModal();
            Item cartobj = context.Items.Single(model => model.ItemId == ItemId);
            if (HttpContext.Session.GetComplexData<List<CartModal>>("cartitem") != null)
            {
                ListcartModals = (List<CartModal>)HttpContext.Session.GetComplexData<List<CartModal>>("cartitem");

            }
            if (ListcartModals.Any(model => model.ItemId == ItemId))
            {
                objcartModal = ListcartModals.Single(model => ItemId == ItemId);
                objcartModal.Quantity = objcartModal.Quantity + 1;
                objcartModal.TotalPrice = objcartModal.Quantity * objcartModal.UnitPrice;
            }
            else
            {
                objcartModal.ItemId = ItemId;
                objcartModal.Image = cartobj.Image;
                objcartModal.Name = cartobj.Name;
                objcartModal.Quantity = 1;
                objcartModal.TotalPrice = cartobj.Price;
                objcartModal.UnitPrice = cartobj.Price;
                ListcartModals.Add(objcartModal);


            }

            HttpContext.Session.SetComplexData("counter", ListcartModals.Count);
            HttpContext.Session.SetComplexData("cartitem", ListcartModals);
            return new JsonResult(new { Success = true, Counter = ListcartModals.Count });
        }
        public IActionResult ShopingCart()
        {

            ListcartModals = (List<CartModal>)HttpContext.Session.GetComplexData<IEnumerable<CartModal>>("cartitem");
            return View(ListcartModals);

        }
        public ActionResult AddOrder()
        {
            int OrderId = 0;
            ListcartModals = (List<CartModal>)HttpContext.Session.GetComplexData<IEnumerable<CartModal>>("cartitem");
            Order orderobj = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = string.Format("{0:ddmmyyyHHmmss}", DateTime.Now)
            };
            context.orders.Add(orderobj);
            context.SaveChanges();
            OrderId = orderobj.OrderId;

            foreach (var item in ListcartModals)
            {
                OrderDetail orderobjdetail = new OrderDetail();
                orderobjdetail.Total = item.TotalPrice;
                orderobjdetail.OrderId =OrderId;
                orderobjdetail.Quantity = item.Quantity;
                orderobjdetail.UnitPrice = item.UnitPrice;
                context.orderDetails.Add(orderobjdetail);

            }
                context.SaveChanges();

            HttpContext.Session.SetComplexData("cartitem", ListcartModals);
            HttpContext.Session.SetComplexData("CartCounter", ListcartModals.Count());
            return RedirectToAction("Index");
        }
        public  IActionResult RemoveFromCart(int ItemId)
        {
            ListcartModals = (List<CartModal>)HttpContext.Session.GetComplexData<IEnumerable<CartModal>>("cartitem");
            var itemRemoce = ListcartModals.Where(a=>a.ItemId== ItemId).FirstOrDefault();
            ListcartModals.Remove(itemRemoce);

            HttpContext.Session.SetComplexData("cartitem",ListcartModals);
            HttpContext.Session.SetComplexData("CartCounter", ListcartModals.Count());
            return RedirectToAction("ShopingCart", "Cart");
        }
        public IActionResult Contact()
        {
            return View();
        }
    }

}

