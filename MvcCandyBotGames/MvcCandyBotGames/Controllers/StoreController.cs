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
    public class StoreController : Controller
    {
        private CandyBotGamesEntities db = new CandyBotGamesEntities();
        // GET: Store
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            var products = from s in db.Products.Include(p => p.Developer).Include(p => p.Genre) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Title);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Title);
                    break;
            }
            return View(products.ToList());
        }

        public ActionResult Genre()
        {
            var genre = db.Genres;
            return View(genre.ToList());
        }

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

        public ActionResult Browse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Genre genreModel = db.Genres.Find(id);
            List<Product> products = new List<Product>();
            var productList = db.Products.Include(p => p.Developer).Include(p => p.Genre);
            foreach (var item in productList.ToList())
            {
                if (item.GenreID == id)
                    products.Add(item);
            }

            return View(products);
        }
    }
}