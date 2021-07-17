using Error404_Web.Models.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Error404_Web.Areas.Admin.Controllers
{
    public class AccountManagerController : Controller
    {
        private AccountBUS AB;

        public AccountManagerController()
        {
            AB = new AccountBUS();
        }
        // GET: Admin/AccountManager
        public ActionResult Acount()
        {
            return View();
        }

        public bool LoginAdmin(string user, string pass)
        {
            AB = new AccountBUS();
            string checkLogin = AB.checkUserAdmin(user, pass);
            if (checkLogin == user)
            {
                Session["userAdmin"] = checkLogin;
                return true;
            }
            return false;
        }
    }
}