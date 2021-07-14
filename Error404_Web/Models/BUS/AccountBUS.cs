using Error404_Web.Models.Error404_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Error404_Web.Models.BUS
{
    public class AccountBUS
    {
        private AppleEntities db;

        public AccountBUS()
        {
            db = new AppleEntities();
        }

        public IEnumerable<TaiKhoan> listAccount()
        {
            return db.TaiKhoan;
        }

        public string checkLogin(string email, string pass)
        {
            Console.WriteLine(email);
            Console.WriteLine(pass);
            var result = db.CheckLogin(email, pass).SingleOrDefault();
            //var result = db.TaiKhoan.Where(t => t.Email == email & t.PW == pass).Select(p => p.Email).SingleOrDefault();
            return result;
        }

        public string getName(string user)
        {
            var result = db.KhachHang.Where(c => c.FullName == user).Select(p => p.FullName).SingleOrDefault();
            return result;
        }

        public string getEmail(string user)
        {
            var result = db.KhachHang.Where(c => c.Email == user).Select(p => p.Email).SingleOrDefault();
            return result;
        }

        public bool checkEmail(string email)
        {
            var result = db.KhachHang.Where(p => p.Email == email).SingleOrDefault();

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool checkSDT(string sdt)
        {
            var result = db.KhachHang.Where(p => p.SDT == sdt).SingleOrDefault();

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public void Signup(string email, string pass, string fullname, string sdt)
        {
            KhachHang kh = new KhachHang
            {
                SDT = sdt,
                FullName = fullname,
                Address = "",
                Email = email,
            };
            db.KhachHang.AddOrUpdate(kh);

            TaiKhoan tk = new TaiKhoan
            {
                Email = email,
                PW = pass,
            };
            db.TaiKhoan.AddOrUpdate(tk);

            db.SaveChanges();
        }

        public KhachHang account(string email)
        {
            return db.KhachHang.Where(p => p.Email == email).FirstOrDefault();
        }

       
        public IEnumerable<Rate> rating(string email)
        {
            return db.Rate.Where(p => p.Email == email);
        }

        public IEnumerable<Order> loadBill(string email)
        {
            return db.Order.Where(p => p.Email == email);
        }



        public void changeInfo(string sdt, string fullname, string address, string email)
        {
            KhachHang a = db.KhachHang.Where(p => p.SDT == sdt).SingleOrDefault();
            a.FullName = fullname;
            a.Address = address;
            a.Email = email;
            db.SaveChanges();
        }

        
        public void changePass(string email, string pass_new)
        {
            TaiKhoan a = db.TaiKhoan.Where(p => p.Email == email).FirstOrDefault();
            a.PW = pass_new;
            db.SaveChanges();
        }

        public string checkUserAdmin(string user, string pass)
        {
            string result = "";
            if (pass == "admin")
            {
                result = user;
            }
            return result;
        }

    }
}