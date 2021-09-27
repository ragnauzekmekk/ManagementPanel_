using ManagementPanel_.Bll.Services;
using ManagementPanel_.Model.ComplexType;
using ManagementPanel_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ManagementPanel_.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _accountService.Login(model);

                if (user.Status)
                {

                    HttpContext.Session["Name"] = user.Object.Name;
                    HttpContext.Session["Surname"] = user.Object.Surname;
                    HttpContext.Session["Admin"] = user.Object.Admin;
                    HttpContext.Session["Email"] = user.Object.Email;

                    FormsAuthentication.SetAuthCookie(user.Object.ID.ToString(), true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Warning = "Kullanıcı kodu ve şifrenizi kontrol ediniz.";
                    return View();
                }
            }
            else
            {
                ViewBag.Warning = "Kullanıcı kodu ve şifrenizi kontrol ediniz.";
                return View();
            }

        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}