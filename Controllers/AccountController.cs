using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using TanvirBakery.Models;
using TanvirBakery.Interface;
using TanvirBakery.Repository;

namespace TanvirBakery.Controllers
{
    public class AccountController : Controller
    {
        IRepository<User> repo = new UsersRepository(new BakeryContext());
     

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            User user = repo.GetAll().Where(u => u.Username == username && u.Password == password).FirstOrDefault();

            if (user != null)
            {
                Session["user_id"] = user.Id;
                Session["username"] = user.Username;
                Session["is_admin"] = user.IsAdmin;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Username/Password is Incorrect";
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.Insert(user);
                    repo.Submit();
                    TempData["New"] = "Account Successfully Created. Please login your Account";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = "Username exists";
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Manage()
        {
            return View(repo.GetById((int)Session["user_id"]));
        }
        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }




    }
}