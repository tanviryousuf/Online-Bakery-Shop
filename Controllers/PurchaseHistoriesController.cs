using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TanvirBakery.Models;

namespace TanvirBakery.Controllers
{
    public class PurchaseHistoriesController : BaseController
    {
        private BakeryContext db = new BakeryContext();

        // GET: PurchaseHistories
        public ActionResult Index()
        {
            var purchaseHistories = db.PurchaseHistories.Include(p => p.Item).Include(p => p.User);
            return View(purchaseHistories.ToList());
        }

        // GET: PurchaseHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseHistory purchaseHistory = db.PurchaseHistories.Find(id);
            if (purchaseHistory == null)
            {
                return HttpNotFound();
            }
            return View(purchaseHistory);
        }

        // GET: PurchaseHistories/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Items, "Id", "Name");
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: PurchaseHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Date,ProductId,BuyerId")] PurchaseHistory purchaseHistory)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseHistories.Add(purchaseHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Items, "Id", "Name", purchaseHistory.ProductId);
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "Username", purchaseHistory.BuyerId);
            return View(purchaseHistory);
        }

        // GET: PurchaseHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseHistory purchaseHistory = db.PurchaseHistories.Find(id);
            if (purchaseHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Items, "Id", "Name", purchaseHistory.ProductId);
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "Username", purchaseHistory.BuyerId);
            return View(purchaseHistory);
        }

        // POST: PurchaseHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Date,ProductId,BuyerId")] PurchaseHistory purchaseHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Items, "Id", "Name", purchaseHistory.ProductId);
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "Username", purchaseHistory.BuyerId);
            return View(purchaseHistory);
        }

        // GET: PurchaseHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseHistory purchaseHistory = db.PurchaseHistories.Find(id);
            if (purchaseHistory == null)
            {
                return HttpNotFound();
            }
            return View(purchaseHistory);
        }

        // POST: PurchaseHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseHistory purchaseHistory = db.PurchaseHistories.Find(id);
            db.PurchaseHistories.Remove(purchaseHistory);
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
