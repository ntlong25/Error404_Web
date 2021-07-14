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

        public SanPham ChiTietSP(string id)
        {
            return db.SanPham.Where(p => p.MaSP == id).SingleOrDefault();
        }

        public IEnumerable<SanPham> LoadProductByCategories(string id)
        {
            return db.SanPham.Where(p => p.LoaiSP.MaDM == id & p.SLTon > 0);
        }


        // Load product of user cart for cart page
        public IEnumerable<ProductOfCartModel> loadProductCart(string email)
        {
            var list = from a in db.SanPham
                       join b in db.Cart
                        on a.MaSP equals b.MaSP
                       where b.Email == email 
                       select new ProductOfCartModel
                       {
                           ProductId = a.MaSP,
                           ProductName = a.TenSP,
                           Img = a.img,
                           ProductPrice = a.DonGia,
                           ProductAmount = b.SL,
                       };

            return list;
        }

    }
}