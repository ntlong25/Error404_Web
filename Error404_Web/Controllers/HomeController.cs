using Error404_Web.Models.BUS;
using Error404_Web.Models.Error404_Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Error404_Web.Controllers
{
    public class HomeController : Controller
    {
        private AppleEntities db;
        private ProductBUS productBUS;
        private AccountBUS accountBUS;
        public HomeController()
        {
            db = new AppleEntities();
            productBUS = new ProductBUS();
            accountBUS = new AccountBUS();
        }

        public ActionResult Home()
        {
            return View();
        }

        //Load sản phẩm theo danh mục
        public ActionResult Category(string id, int page = 1, int pageSize = 12)
        {
            Session["CategoryId"] = id;
            var ds = productBUS.LoadProductByCategories(id).OrderBy(p => p.MaSP).ToPagedList(page, pageSize);
            return View(ds);
        }

        //Load 1 sản phẩm
        public ActionResult Details(string id)
        {
            var db = productBUS.ChiTietSP(id);
            return View(db);
        }

        //Vào giỏ hàng
        public ActionResult Cart(string user)
        {
            var db = new ProductBUS();

            var result = db.loadProductCart(user);

            return View(result);
        }

    }
}