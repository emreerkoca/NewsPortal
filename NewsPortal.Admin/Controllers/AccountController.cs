using NewsPortal.Core.Infstructure;
using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Admin.Controllers
{
    public class AccountController : Controller
    {
        #region User
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userControl = _userRepository.GetMany(x => x.Email == user.Email && x.Password == user.Password && x.IsActive == true).SingleOrDefault();
            if(userControl != null)
            {
                if (userControl.Role.RoleName == "Admin")
                {
                    Session["UserID"] = userControl.ID;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Message = "Unauthorized User";
                return View();
            }
            ViewBag.Message = "User is not found!";
            return View();
        }
    }
}