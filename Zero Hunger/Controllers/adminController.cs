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
    public class adminController : Controller
    {
        // GET: employee
        [adminAccess]
        public ActionResult Index()
        {
            var db = new zero_hungerEntities();
            return View(db.employees.ToList());
        }
        [HttpPost]
        public ActionResult login(loginDTO obj)
        {
            if(ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                var user = (from u in db.admins
                            where 
                                u.username.Equals(obj.username) &&
                                u.password.Equals(obj.password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user.username;
                    Session["id"] = user.id;
                    Session["type"] = "admin";
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
        public ActionResult login()
        {
            return View();
        }
        public ActionResult logout()
        {
            Session.Clear();
            return RedirectToAction("login");
        }
        [HttpGet]
        [adminAccess]
        public ActionResult addemployee() 
        {
            return View();
        }
        [HttpPost]
        [adminAccess]
        public ActionResult addemployee(addemployeeDTO empModel) 
        {
            if(ModelState.IsValid)
            {
                var db = new zero_hungerEntities();
                db.employees.Add(convert(empModel));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empModel);
        }
        employee convert(addemployeeDTO empDTO)
        {
            employee emp = new employee()
            {
                name = empDTO.name,
                email = empDTO.email,
                phone = empDTO.phone,
                address = empDTO.address,
                dob = empDTO.dob,
                username = empDTO.username,
                password = empDTO.password,
                admin_id = (int?)Session["id"]
            };
            return emp;
        }
    }
}