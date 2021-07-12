using Error404_Web.Models.BUS;
using Error404_Web.Models.Error404_Entity;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Error404_Web.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryBUS categoryBUS;
        public CategoryController()
        {
            categoryBUS = new CategoryBUS();
        }
        // GET: Category
        public ActionResult Index(String id, int page = 1, int pageSize = 12)
        {
            Session["CategoryId"] = id;
            var ds = categoryBUS.ChiTiet(id).OrderBy(p => p.MaSP).ToPagedList(page, pageSize);
            return View(ds);
        }
        
    }
}