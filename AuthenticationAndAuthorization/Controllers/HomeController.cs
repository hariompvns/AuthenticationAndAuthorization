using AuthenticationAndAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthenticationAndAuthorization.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(User user)
        {

            UserRepository usersEntities = new UserRepository();
            Role role = new Role();
            User roleUser = new User();
            roleUser = usersEntities.AllUser().Where(x => x.Username == user.Username && x.Password == user.Password).SingleOrDefault();
            
            if (roleUser!=null)
            {
                 role = usersEntities.AllRoles().Where(x => x.RoleId == roleUser.RoleId).SingleOrDefault();
            }
            else
            {
                roleUser = new User();
                roleUser.UserId = 10;
                role.RoleName = "";
            }
            string message = string.Empty;
            switch (roleUser.UserId)
            {
                case 10:
                    message = "Username and/or password is incorrect.";
                    break;
                case -2:
                    message = "Account has not been activated.";
                    break;
                default:
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(2880), user.RememberMe, role.RoleName, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                    {
                        return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
                    }
                    else
                    {
                        return RedirectToAction("Profile");
                    }
            }

            ViewBag.Message = message;
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}