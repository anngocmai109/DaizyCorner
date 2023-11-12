using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        // GET: Wishlist

        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Wishlist> items = db.Wishlists.Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.CreatedDate);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        [HttpPost]
        //[AllowAnonymous]
        public ActionResult PostWishlist(int ProductId)
        {
            if(Request.IsAuthenticated == false)
            {
                return Json(new { Success = false,Message="Bạn cần đăng nhập tài khoản!" });
            }
            var checkItem = db.Wishlists.FirstOrDefault(x => x.ProductId == ProductId && x.UserName == User.Identity.Name);
            if (checkItem != null)
            {
                return Json(new { Success = false, Message = "Đã thêm vào mục yêu thích!" });
            }
            var item = new Wishlist();
            item.ProductId = ProductId;
            item.UserName = User.Identity.Name;
            item.CreatedDate = DateTime.Now;
            db.Wishlists.Add(item);
            db.SaveChanges();
            return Json(new { Success = true });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostDeleteWishlist(int ProductId)
        {
            var checkItem = db.Wishlists.FirstOrDefault(x => x.ProductId == ProductId && x.UserName == User.Identity.Name);

            if (checkItem != null)
            {
                db.Wishlists.Remove(checkItem);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Bỏ yêu thích thành công." });
            }
            return Json(new { Success = false, Message = "Không thể xóa sản phẩm khỏi danh sách yêu thích" });
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

        
    }
}