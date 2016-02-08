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
            return View(CollectionViewModel.FromCollection(collection));
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
