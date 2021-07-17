using Error404_Web.Models;
using Error404_Web.Models.Error404_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Error404_Web.Areas.Admin.Models
{
    public class ProductManagerBUS
    {
        private AppleEntities db;

        public ProductManagerBUS()
        {
            db = new AppleEntities();
        }
        public IEnumerable<SanPham> loadProduct()
        {
            var result = db.SanPham;

            return result;
        }
        public SanPham ChiTietSP(string id)
        {
            var result = db.SanPham.Where(p => p.MaSP == id).FirstOrDefault();

            return result;
        }

        public void delSanPham(string id)
        {
            var product = db.SanPham.Where(t => t.MaSP == id).FirstOrDefault();

            SanPham p = new SanPham
            {
                MaSP=product.MaSP,
                MaLSP=product.MaLSP,
                img=product.img,
                img2=product.img2,
                TenSP=product.TenSP,
                DonGia=product.DonGia,
                GiaKM=product.GiaKM,
                SLTon=product.SLTon,
                Description=product.Description,
                MoreInfo=product.MoreInfo,
                DateAdd=product.DateAdd
            };
            db.SanPham.AddOrUpdate(p);
            db.SaveChanges();
        }

        public IEnumerable<LoaiSP> loadLoaiSP()
        {
            return db.LoaiSP;
        }

        public void editSanPham(string MaSP, string MaLSP, string img, string img2,string TenSP, float DonGia, float GiaKM, int SLTon, string Description, string moreInfo)
        {
            var dateadd = db.SanPham.Where(t => t.MaSP == MaSP).Select(c => c.DateAdd).FirstOrDefault();
            SanPham p = new SanPham
            {
                MaSP = MaSP,
                MaLSP = MaLSP,
                img = img,
                img2 = img2,
                TenSP = TenSP,
                DonGia = DonGia,
                GiaKM = GiaKM,
                SLTon = (short?)SLTon,
                Description = Description,
                MoreInfo = moreInfo,
                DateAdd = dateadd
            };
            db.SanPham.AddOrUpdate(p);
            db.SaveChanges();
        }

        public void addSanPham (string MaLSP, string img, string img2,string TenSP, float DonGia, float GiaKM, int SLTon, string Description, string moreInfo)
        {
            DateTime time = DateTime.Now;
            SanPham p = new SanPham
            {
                MaLSP = MaLSP,
                img = img,
                img2 = img2,
                TenSP = TenSP,
                DonGia = DonGia,
                GiaKM = GiaKM,
                SLTon = (short?)SLTon,
                Description = Description,
                MoreInfo = moreInfo,
                DateAdd = time
            };
            db.SanPham.AddOrUpdate(p);
            db.SaveChanges();
        }


    }
}