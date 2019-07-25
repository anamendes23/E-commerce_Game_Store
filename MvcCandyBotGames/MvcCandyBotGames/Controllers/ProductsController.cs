using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcCandyBotGames.Models;

namespace MvcCandyBotGames.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class ProductsController : Controller
    {
        private CandyBotGamesEntities db = new CandyBotGamesEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Developer).Include(p => p.Genre);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create        
        public ActionResult Create()
        {
            ViewBag.DeveloperID = new SelectList(db.Developers.OrderBy(a=>a.Name), "DeveloperID", "Name");
            ViewBag.GenreID = new SelectList(db.Genres.OrderBy(a => a.Name), "GenreID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,GenreID,DeveloperID,Title,Price,GameArtUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "Name", product.DeveloperID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", product.GenreID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "Name", product.DeveloperID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", product.GenreID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,GenreID,DeveloperID,Title,Price,GameArtUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "Name", product.DeveloperID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", product.GenreID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
