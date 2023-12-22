using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        // GET: Review
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]        
        public ActionResult _Review(int productId)
        {
            ViewBag.ProductId = productId;
            var item = new ReviewProduct();
            if(User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                if (user != null)
                {
                    item.Email = user.Email;
                    item.FullName = user.FullName;
                    item.UserName = user.UserName;
                }
                return PartialView(item);
            }
            return PartialView();
        }

        public ActionResult LichSuDonHang()
        {
            if(User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                var items = db.Orders.Where(x => x.CustomerId == user.Id).OrderByDescending(s => s.CreatedDate).ToList();
                return PartialView(items);
            }
            return PartialView();
        }

        public ActionResult OrderDetail(int orderid)
        {
            var item = db.Orders.Find(orderid);
            if (User.Identity.IsAuthenticated && item != null)
            {
                return View(item);
            }
            return View();
        }

        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [AllowAnonymous]
        public ActionResult _Load_Review(int productId)
        {
            var item = db.Reviews.Where(x => x.ProductId == productId).OrderByDescending(x => x.CreatedDate).ToList() ;
            ViewBag.Count = item.Count;
            return PartialView(item);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostReview(ReviewProduct req)
        {
            if(ModelState.IsValid)
            {
                req.CreatedDate = DateTime.Now;
                db.Reviews.Add(req);
                db.SaveChanges();
                return Json(new {Success = true});
            }
            return Json(new { Success = false });
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}