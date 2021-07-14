using Error404_Web.Models.BUS;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Error404_Web.Controllers
{
    public class ProductController : Controller
    {
        private ProductBUS productBUS;
        public ProductController()
        {
            productBUS = new ProductBUS();
        }
        
    }
}
