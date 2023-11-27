using System;
using System.Linq;
using System.Web.Mvc;
using WebBanHangOnline.Common;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index(string search = "", int curentPage = 1, int pageSize = 8, string orderBy = "")
        {
            ProductViewModel productViews = new ProductViewModel();

            Pagination pagination = new Pagination()
            {
                CurrentPage = curentPage,
                PageSize = pageSize,
                OrderBy = orderBy
            };
            IQueryable<Product> items = db.Products.Where(p => p.Quantity > 0).OrderBy(x => x.Id);

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                items = items.Where(p => p.Title.Trim().ToLower().Contains(search) ||
                                         p.Description.Trim().ToLower().Contains(search));
            }

            if (!string.IsNullOrEmpty(orderBy))
                switch (orderBy)
                {
                    case "price":
                        items = items.OrderBy(x => x.PriceSale);
                        pagination.OrderByStr = "Giá";
                        break;

                    case "name":
                        items = items.OrderBy(x => x.Title);
                        pagination.OrderByStr = "Tên sản phẩm";
                        break;
                }


            pagination.TotalPages = (items?.Count() == 0 ? 0 : Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(items.Count()) / pageSize)));

            productViews.Pagination = pagination;
            productViews.Products = items.Skip((curentPage - 1)* pageSize).Take(pageSize).ToList();

            return View(productViews);
        }

        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            var countReview = db.Reviews.Where(x => x.ProductId == id).Count();
            ViewBag.CountReview = countReview;
            return View(item);
        }
        public ActionResult ProductCategory(string alias, int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);
        }
        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(10).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).OrderByDescending(s => s.CreatedDate).ToList();
            return PartialView(items);
        }
        public ActionResult search()
        {
            return PartialView();
        }

    }
}