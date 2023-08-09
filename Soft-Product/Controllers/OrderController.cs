using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soft_Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext context;

        public OrderController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var orders = context.orders.Include(o => o.OrderDetails).ToList();
            return View(orders);
        }
        public IActionResult Confirm(int id)
        {
            var order = context.orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.IsConfirmed = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Dispatch(int id)
        {
            var order = context.orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.IsDispatched = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Deliver(int id)
        {
            var order = context.orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.IsDelivered = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public  IActionResult Details(int orderid)
        {
            var orders = context.orderDetails.Where(o => o.OrderId== orderid).ToList();
            return View(orders);
        }

    }
}
