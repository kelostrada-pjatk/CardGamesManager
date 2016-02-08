using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CardGames.MVC.Models.CardGames;
using Microsoft.AspNet.Identity;

namespace CardGames.MVC.Controllers
{
    [Authorize]
    public class CollectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collections
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var cardLists = db.CardLists.OfType<Collection>().Include(c => c.User).Where(c => c.UserId == userId);
            return View(cardLists.ToList());
        }

        // GET: Collections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Collection collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == id && c.UserId == userId);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Collections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Public,Deck")] CollectionViewModel collectionViewModel)
        {
            if (ModelState.IsValid)
            {
                var collection = collectionViewModel.GetCollection();
                collection.UserId = User.Identity.GetUserId();
                db.CardLists.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(collectionViewModel);
        }

        // GET: Collections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Collection collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == id && c.UserId == userId);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(CollectionViewModel.FromCollection(collection));
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Public,Deck")] CollectionViewModel collectionViewModel)
        {
            if (ModelState.IsValid)
            {
                var collection = collectionViewModel.GetCollection();
                collection.UserId = User.Identity.GetUserId();
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collectionViewModel);
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

        public ActionResult AddCard(int? id)
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

            var gameId = db.Games.Select(g => g.Id).Min();
            
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", gameId);
            ViewBag.EditionId = new SelectList(db.Editions.Where(e => e.GameId == gameId), "Id", "Name");
            ViewBag.CardId = new SelectList(new Card[0], "Id", "Name");
            ViewBag.Collection = collection;
            return View(new AddCardViewModel {CollectionId = id.Value} );
        }

        [HttpPost, ActionName("AddCard")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCard([Bind(Include = "CollectionId,CardId,EditionId,GameId,Quantity")] AddCardViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Collection collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == viewModel.CollectionId);

            if (collection == null)
            {
                return HttpNotFound();
            }
            
            if (viewModel.CardId != null)
            {
                var card = db.Cards.Find(viewModel.CardId);
                collection.AddCard(card, viewModel.Quantity);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = viewModel.CollectionId });
            }

            if (viewModel.GameId == null)
            {
                viewModel.GameId = db.Games.Select(g => g.Id).Min();
            }
            
            ViewBag.GameId = new SelectList(db.Games, "Id", "Name", viewModel.GameId);
            ViewBag.EditionId = new SelectList(db.Editions.Where(e => e.GameId == viewModel.GameId), "Id", "Name");
            ViewBag.CardId = new SelectList(new Card[0], "Id", "Name");

            if (viewModel.GameId != null)
            {
                ViewBag.EditionId = new SelectList(db.Editions.Where(edition => edition.GameId == viewModel.GameId), 
                    "Id", "Name", viewModel.GameId);
            }

            if (viewModel.EditionId != null)
            {
                var edition = db.Editions.Find(viewModel.EditionId);
                if (viewModel.GameId == edition.GameId)
                {
                    ViewBag.CardId = new SelectList(db.Cards.ToList().Where(c => c.Edition.Id == viewModel.EditionId),
                        "Id", "Name", viewModel.CardId);
                }
            }
            
            ViewBag.Collection = collection;

            return View(viewModel);
        }

        [HttpGet, ActionName("Add")]
        public ActionResult Add(int id, int cardId)
        {
            var collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == id);

            if (collection == null)
            {
                return HttpNotFound();
            }

            var card = db.Cards.Find(cardId);

            if (card == null)
            {
                return HttpNotFound();
            }

            collection.AddCard(card);
            db.SaveChanges();

            return RedirectToAction("Details", new {id = id});
        }

        [HttpGet, ActionName("Remove")]
        public ActionResult Remove(int id, int cardId)
        {
            var collection = db.CardLists.OfType<Collection>().FirstOrDefault(c => c.Id == id);

            if (collection == null)
            {
                return HttpNotFound();
            }

            var card = db.Cards.Find(cardId);

            if (card == null)
            {
                return HttpNotFound();
            }

            collection.RemoveCard(card);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = id });
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
