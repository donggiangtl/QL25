using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataIO;
using DataIO.MyModel;

namespace QL25.Controllers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _allowedRoles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            _allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userRole = httpContext.Session["UserRole"] as string;

            if (userRole == null)
            {
                return false; // User is not logged in
            }

            return _allowedRoles.Contains(userRole);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }

    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            // Clear authentication cookie (if using Forms Auth)
            FormsAuthentication.SignOut();
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(PassWordModel model)
        {
            if (ModelState.IsValid)
            {
                int dV_ID;
                switch (Helper.TestUserAccount(model, out dV_ID)) {
                    case TestAccount.wrong_username:
                        ViewBag.Notification = "Tài khoản hoặc mật khẩu không đúng!";
                        return View();
                    case TestAccount.wrong_password:
                        ViewBag.Notification = "Tài khoản hoặc mật khẩu không đúng!";
                        return View();
                    case TestAccount.ok:
                        Session["UserRole"] = "User";
                        Session["dV_ID"] = dV_ID;
                        FormsAuthentication.SetAuthCookie(model.UerName, false); // false = non-persistent
                        TempData["UserName"] = model.UerName;
                        TempData["dV_ID"] = dV_ID;
                        return RedirectToAction("Index", "User", new { dV_ID = dV_ID});
                }                
            }
            ViewBag.Message = "Invalid credentials";
            return View();
        }
        

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(PassWordModel model)
        {
            if (ModelState.IsValid)
            {
                switch (Helper.TestAdminAccount(model))
                {
                    case TestAccount.wrong_username:
                        ViewBag.Notification = "Tài khoản không đúng!";
                        return View();
                    case TestAccount.wrong_password:
                        ViewBag.Notification = "Mật khẩu không đúng!";
                        return View();
                    case TestAccount.disable:
                        ViewBag.Notification = "Disable!";
                        return View();
                    case TestAccount.ok:
                        Session["UserRole"] = "Admin";
                        FormsAuthentication.SetAuthCookie(model.UerName, false); // false = non-persistent
                        return RedirectToAction("Index", "AdminSystem");                       
                }
            }
            ViewBag.Message = "Invalid credentials";
            return View();
        }
        // Logout
        public ActionResult Logout()
        {
            // Clear session
            Session.Clear();
            Session.Abandon();

            // Clear authentication cookie (if using Forms Auth)
            FormsAuthentication.SignOut();

            // Optionally, clear all cookies
            foreach (var cookie in Request.Cookies.AllKeys)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}