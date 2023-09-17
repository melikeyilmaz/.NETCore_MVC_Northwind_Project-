using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Odev_1_MelikeYilmaz.Models;

namespace Odev_1_MelikeYilmaz.Controllers
{
    public class OrdersController : Controller
    {
        private readonly NorthwindDbContext _context;

        public OrdersController(NorthwindDbContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            var northwindDbContext = _context.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.ShipViaNavigation);
            return View(await northwindDbContext.ToListAsync());
        }


        [HttpGet]
        public IActionResult List(int EmployeeId)
        {
            // Orders ve Employees tablolarını birleştirerek siparişleri alın
            var orders = (from o in _context.Orders
                              join e in _context.Employees on o.EmployeeId equals e.EmployeeId
                              where o.EmployeeId == EmployeeId
                              select new Order
                              {
                                  OrderId = o.OrderId,
                                  OrderDate = o.OrderDate,
                                  ShipCountry = o.ShipCountry
                              })
                              .OrderBy(o => o.OrderId)
                              .ToList();

            return View(orders);
        }       

    }
}
