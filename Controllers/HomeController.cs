using TanvirBakery.Interface;
using TanvirBakery.Models;
using TanvirBakery.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TanvirBakery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

       
                IRepository<Item> repo = new ProductsRepository(new BakeryContext());

            if (!String.IsNullOrEmpty(Request["keyword"]))
            {
                string keyword = Request["keyword"].ToLower();
                return View(repo.GetAll().Where(p => p.Name.ToLower().Contains(keyword) || p.Brand.Name.ToLower().Contains(keyword)));
            }
            else
               // return View(repo.GetAll());

           return View(repo.GetAll());
        }

        /*public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }*/

        /*public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/
    }
}