using ManagementPanel_.Bll.Services;
using ManagementPanel_.Model.ComplexType;
using ManagementPanel_.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementPanel_.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUsersService _usersService;
        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public ActionResult Edit()
        {
            Session["Page"] = "Kullanıcı Ekle";
            return View();
        }

        
        public ActionResult EditUser(int id)
        {
            var model = _usersService.GetUser(id);
            return PartialView(model.Object);
        }

        [HttpPost]
        public JsonResult AddUser(UserModel model)
        {
            var result = _usersService.AddUser(model);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateUser(UserModel model)
        {
            var result = _usersService.UpdateUser(model);
            return Json(result);
        }
        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            var result = _usersService.DeleteUser(id);
            return Json(result);
        }
        [HttpPost]
        public JsonResult GetEmailPhoneUsernameControl(string email, string phone, string username)
        {
            var result = _usersService.GetEmailPhoneUsernameControl(email, phone, username);
            return Json(result);
        }
    }
}