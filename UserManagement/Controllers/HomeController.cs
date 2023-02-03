using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class HomeController : Controller
    {
        UniversityEntities db = new UniversityEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Signup(users model)
        {
            ViewBag.Message = "Your contact page.";
            if(db.users.Any(x => x.username == model.username))
            {
                ViewBag.Notifications = "This account has already existed";
            }
            else
            {
                db.users.Add(model);
                db.SaveChanges();
                Session["id"] = model.id.ToString();
                Session["username"] = model.username.ToString();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(users model)
        {
            var checkLogin = db.users.Where(x => x.username.Equals(model.username) && x.passwords.Equals(model.passwords)).FirstOrDefault();

            if(checkLogin != null )
            {
                Session["id"] = model.id.ToString();
                Session["username"] = model.username.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notifications = "Wrong Username or password";
            }
            return View();
        }
    }
}