﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CardGames.MVC.Models.CardGames;

namespace CardGames.MVC.Controllers
{
    public class CardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Cards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            var cardInList = card.CardInCardLists.FirstOrDefault(l => l.CardList is EditionCardList);
            return View(cardInList);
        }

        // GET: Cards/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Edition = db.Editions.Find(id);
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditionId,Name,Description,Number")] CardViewModel cardViewModel)
        {
            var edition = db.Editions.Find(cardViewModel.EditionId);
            ViewBag.Edition = edition;
            if (ModelState.IsValid)
            {
                var card = db.Cards.Create();
                card.Name = cardViewModel.Name;
                card.Description = cardViewModel.Description;
                db.SaveChanges();

                db.Cards.Add(card);

                var cardInList = edition.EditionCardList.AddCard(card, 1, cardViewModel.Number);

                //db.Entry(cardInList).State = EntityState.Modified; 

                db.SaveChanges();
                return RedirectToAction("Details", "Editions", new { id = edition.Id });
            }

            return View(cardViewModel);
        }

        // GET: Cards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            var cardInList = card.CardInCardLists.FirstOrDefault(l => l.CardList is EditionCardList);
            var cardViewModel = new CardViewModel
            {
                CardId = card.Id,
                Description = card.Description,
                Name = card.Name,
                EditionId = ((EditionCardList)cardInList.CardList).Edition.Id,
                Number = cardInList.Number
            };
            return View(cardViewModel);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CardId,Name,Description,Number,EditionId")] CardViewModel cardViewModel)
        {
            if (ModelState.IsValid)
            {
                var card = db.Cards.Find(cardViewModel.CardId);
                card.Name = cardViewModel.Name;
                card.Description = cardViewModel.Description;

                var cardInList = card.CardInCardLists.First(cl => cl.CardList is EditionCardList);
                cardInList.Number = cardViewModel.Number;

                //db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Editions", new { id = cardViewModel.EditionId});
            }
            return View(cardViewModel);
        }

        // GET: Cards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card card = db.Cards.Find(id);
            var edition = card.Edition;
            db.Cards.Remove(card);
            db.SaveChanges();
            return RedirectToAction("Details", "Editions", new {id = edition.Id});
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
