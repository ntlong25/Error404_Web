using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Error404_Web.Models;
using Error404_Web.Models.BUS;
using Error404_Web.Models.Error404_Entity;
using System.Collections.Generic;

namespace Error404_Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private AccountBUS accountBUS;

        public AccountController()
        {
            accountBUS = new AccountBUS();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult testAjax()
        {
            AccountBUS a = new AccountBUS();

            return PartialView(a.listAccount());
        }

        public bool temp_Login(string email, string pass)
        {
            AccountBUS AB = new AccountBUS();

            string checkLogin = AB.checkLogin(email, pass);
            if (checkLogin == email)
            {
                Session["email"] = checkLogin;
                Session["fullname"] = accountBUS.getName(email);
                return true;
            }
            return false;
        }

        public bool checkPass(string user, string pass)
        {
            string checkLogin = accountBUS.checkLogin(user, pass);
            if (checkLogin == user)
            {
                return true;
            }
            return false;
        }

        public void changePass(string user, string pass_new)
        {
            accountBUS.changePass(user, pass_new);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["email"] = null;
            Session["fullname"] = null;

            return RedirectToAction("Index", "Home");
        }

        public bool checkEmail(string email)
        {
            var result = accountBUS.checkEmail(email);
            return result;
        }

        public bool checkSDT(string sdt)
        {
            var result = accountBUS.checkSDT(sdt);
            return result;
        }

        [HttpPost]
        public bool Signup(string email, string pass, string fullname, string sdt)
        {
            if (checkEmail(email) || checkSDT(sdt))
                return false;

            accountBUS.Signup(email, pass, fullname, sdt);
            return true;
        }

        public IEnumerable<Order> loadBill(string user)
        {
            var result = accountBUS.loadBill(user);

            return result;
        }


        public void changeInfo(string sdt, string fullname, string address, string email)
        {
            accountBUS.changeInfo(sdt, fullname, address, email);

            Session["fullname"] = fullname;
            Session["email"] = email;
        }


        // dùng thêm địa chỉ 
        public ActionResult formAddAddress()
        {
            return PartialView();
        }

        

    }
}