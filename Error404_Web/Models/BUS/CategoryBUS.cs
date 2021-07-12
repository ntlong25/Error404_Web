using Error404_Web.Models.Error404_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Error404_Web.Models.BUS
{
    public class CategoryBUS
    {
        private AppleEntities db;

        public CategoryBUS()
        {
            db = new AppleEntities();
        }

        public IEnumerable<SanPham> ChiTiet(String id)
        {
            return db.SanPham.Where(p => p.LoaiSP.MaDM == id & p.SLTon > 0);
        }
    }
}