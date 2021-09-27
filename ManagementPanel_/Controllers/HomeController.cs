using ManagementPanel_.Bll.Services;
using ManagementPanel_.Data.EntityFramework;
using ManagementPanel_.Model.ComplexType;
using ManagementPanel_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementPanel_.Controllers
{
    [_SessionControl]
    public class HomeController : Controller
    {
        private readonly IUsersService _usersService;
        public HomeController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public ActionResult Index()
        {
            Session["Page"] = "Kullanıcı Listesi";
            return View();
        }

        [HttpPost]
        public JsonResult GetUserList()
        {
            var userList = _usersService.GetUserList();
            return Json(userList.ObjectResult);
        }

    }
}