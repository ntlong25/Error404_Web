using Error404_Web.Areas.Admin.Models;
using Error404_Web.Models.BUS;
using Error404_Web.Models.Error404_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Error404_Web.Areas.Admin.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductManagerBUS PS;

        public ProductManagerController()
        {
            PS = new ProductManagerBUS();
        }
        public ActionResult ListProduct()
        {
            return View();
        }
        public IEnumerable<SanPham> loadProduct()
        {
            var result = PS.loadProduct();

            return result;
        }

        public ActionResult ChiTietSP(string id)
        {
            var result = PS.ChiTietSP(id);

            return PartialView(result);
        }

        public void delSanPham(string id)
        {
            PS.delSanPham(id);
        }

        public ActionResult editSanPham(string id)
        {
            var result = PS.ChiTietSP(id);

            return PartialView(result);
        }

        public IEnumerable<LoaiSP> loadLoaiSP()
        {
            return PS.loadLoaiSP();
        }
        public void upProduct(string MaSP, string MaLSP, string img, string img2, string TenSP, float DonGia, float GiaKM, int SLTon, string description, string moreInfo)
        {
            PS.editSanPham(MaSP,MaLSP, img, img2, TenSP, DonGia, GiaKM, SLTon, description, moreInfo);
        }
        public ActionResult addProduct()
        {
            return PartialView();
        }

        public void addSP(string MaLSP,string img, string img2, string TenSP,float DonGia, float GiaKM,int SLTon,string description,string moreInfo)
        {
            PS.addSanPham(MaLSP,img, img2, TenSP,DonGia,GiaKM,SLTon,description,moreInfo);
        }
    }
}