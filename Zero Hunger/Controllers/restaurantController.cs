using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger.DB;
using Zero_Hunger.Models;

namespace Zero_Hunger.Controllers
{
    public class restaurantController : Controller
    {
        // GET: restaurant
        [HttpGet]
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
                var user = (from u in db.restaurants
                            where
                                u.username.Equals(obj.username) &&
                                u.password.Equals(obj.password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user.username;
                    Session["id"] = user.id;
                    Session["type"] = "restaurant";
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
        [HttpGet]
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signup(restSignupDTO obj)
        {
            if (ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                db.restaurants.Add(convert(obj));
                db.SaveChanges();
                return RedirectToAction("login");
            }
            return View(obj);
        }
        restaurant convert(restSignupDTO obj)
        {
            return new restaurant()
            {
                name = obj.name,
                email = obj.email,
                phone = obj.phone,
                address = obj.address,
                username = obj.username,
                password = obj.password
            };
        }
    }
}