using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login1()
        {

            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login1(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                //var user = db.Users.SingleOrDefault(t => t.UserName == model.Name && t.Password == model.Password);
                //if (user != null)
                //{
                //    FormsAuthentication.SetAuthCookie(model.Name, false);//将用户名放入Cookie中

                //    return RedirectToAction("Index");
                //}
                //else
                //{
                //    ModelState.AddModelError("Name", "用户名不存在!");
                //}
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}