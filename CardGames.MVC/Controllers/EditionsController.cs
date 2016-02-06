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

namespace CardGames.MVC.Controllers
{
    public class EditionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Editions
        public ActionResult Index()
        {
            var editions = db.Editions.Include(e => e.EditionCardList).Include(e => e.Game);
            return View(editions.ToList());
        }

        // GET: Editions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // GET: Editions/Create
        public ActionResult Create()
        {
            ViewBag.EditionCardListId = new SelectList(db.CardLists, "Id", "Name");
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name");
            return View();
        }

        // POST: Editions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ReleaseYear,GameId")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                edition.EditionCardList = new EditionCardList {Name = edition.Name};
                db.Editions.Add(edition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // ViewBag.EditionCardListId = new SelectList(db.CardLists, "Id", "Name", edition.EditionCardListId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", edition.GameId);
            return View(edition);
        }

        // GET: Editions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditionCardListId = new SelectList(db.CardLists, "Id", "Name", edition.EditionCardListId);
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", edition.GameId);
            return View(edition);
        }

        // POST: Editions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ReleaseYear,GameId")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(edition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", edition.GameId);
            return View(edition);
        }

        // GET: Editions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Editions.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // POST: Editions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Edition edition = db.Editions.Find(id);
            db.Editions.Remove(edition);
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
