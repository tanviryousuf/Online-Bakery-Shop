using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TanvirBakery.Interface;
using TanvirBakery.Models;
using TanvirBakery.Repository;

namespace TanvirBakery.Controllers
{
    public class ItemsController : BaseController
    {
        IRepository<Item> repo = new ProductsRepository(new BakeryContext());
        private BakeryContext db = new BakeryContext();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Brand).Include(i => i.Category);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);

        }
           

[HttpPost]
        [ActionName("Details")]
        public ActionResult Detail(int? id)
        {
            Item p = db.Items.Find(id);
            p.Quantity--;
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            TempData["success"] = "Your Purchase is Successful " + db.Items.Find(id).Price + " Tk.";
            return Redirect("/items/Details/" + p.Id);
        }
        
        public ActionResult Buy(int id)
        {
           int quant = 1;
           BakeryContext mmc = new BakeryContext();
            Item p = repo.GetById(id);
            if (p.Quantity <= 1)
            {
                ViewBag.Message = "Not enough item";
                return View();
            }
            IRepository<User> user = new UsersRepository(new BakeryContext());
            PurchaseHistory ph = new PurchaseHistory();
            ph.BuyerId = (int)Session["user_id"];
            ph.Date = System.DateTime.Now;
            ph.Price = p.Price * quant;
            ph.ProductId = p.Id;
            p.Quantity -= quant;
            mmc.PurchaseHistories.Add(ph);
            mmc.SaveChanges();
            repo.Update(p);
            repo.Submit();
            ViewBag.Message = "Purchase sucessful";
            return View();
        }



















        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Quantity,Image,Price,CategoryId,BrandId")] Item item)
        {
            if (ModelState.IsValid)
            {
                
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", item.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", item.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Quantity,Image,Price,CategoryId,BrandId")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", item.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
