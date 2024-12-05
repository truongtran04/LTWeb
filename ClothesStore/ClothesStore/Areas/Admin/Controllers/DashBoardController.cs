using ClothesStore.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothesStore.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        private TESTEntities db = new TESTEntities();

        private int TotalClothes()
        {
            return db.Clothes.Where(clo => clo.IsDeleted == false).Count();
        }

        private decimal TotalAmountOnDay(DateTime date)
        {
            return db.Orders
                .Where(o => o.CreatedAt.HasValue && DbFunctions.TruncateTime(o.CreatedAt) == date.Date)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;
        }
        
        private decimal TotalAmountOnMonth(DateTime date)
        {
            return db.Orders
                .Where(o => o.CreatedAt.HasValue && DbFunctions.TruncateTime(o.CreatedAt) == date.Date)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;
        }

        private int TotalOrderOnDay(DateTime date)
        {
            return db.Orders
                .Where(o => o.CreatedAt.HasValue && DbFunctions.TruncateTime(o.CreatedAt) == date.Date)
                .Count();
        }

        private int CountCustomer()
        {
            return db.UserRoles.Where(us => us.Role == "client").Count();
        }

        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            DateTime date = DateTime.Now.Date;
            ViewBag.Total = TotalAmountOnDay(date); // Chỉ sử dụng ngày
            ViewBag.Clothes = TotalClothes();
            ViewBag.Order = TotalOrderOnDay(date);
            ViewBag.Customer = CountCustomer();
            return View();
        }

    }
}
