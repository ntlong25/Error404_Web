using Error404_Web.Models.Error404_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Error404_Web.Models.BUS
{
    public class ProductBUS
    {
        private AppleEntities db;

        public ProductBUS()
        {
            db = new AppleEntities();
        }

        public SanPham ChiTiet(string id)
        {
            return db.SanPham.Where(p => p.MaSP == id).SingleOrDefault();
        }
    }
}