using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CardGames.MVC;
using CardGames.MVC.Models.CardGames;
using Microsoft.AspNet.Identity;

namespace CardGames.MVC.Controllers
{
    public class CollectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collections
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var cardLists = db.CardLists.OfType<Collection>().Include(c => c.User).Where(c => c.UserId == userId);
            return View(cardLists.ToList());
        }

        // GET: Collections/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Collections/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Public,UserId")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.CardLists.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", collection.UserId);
            return View(collection);
        }

        // GET: Collections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", collection.UserId);
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Public,UserId")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", collection.UserId);
            return View(collection);
        }

        // GET: Collections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Collection collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == id);
            db.CardLists.Remove(collection);
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
