using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.Auth;
using Zero_Hunger.DB;
using Zero_Hunger.Models;

namespace Zero_Hunger.Controllers
{
    public class employeeController : Controller
    {
        // GET: employee
        [HttpGet]
        [empAccess]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(loginDTO obj)
        {
            if (ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                var user = (from u in db.employees
                            where
                                u.username.Equals(obj.username) &&
                                u.password.Equals(obj.password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user.username;
                    Session["id"] = user.id;
                    Session["type"] = "employee";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "Invalid credential";
                }
            }
            return View(obj);
        }
        [HttpGet]
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("login");
        }
    }
}