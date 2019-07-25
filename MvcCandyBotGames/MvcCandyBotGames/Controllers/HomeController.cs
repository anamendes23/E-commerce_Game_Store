using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCandyBotGames.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult ManagerView()
        {
            return View();
        }

        public ActionResult Games()
        {
            /* Server Explorer > Data Connections > Default Connection > Tables > AspNetRoles
             * Server Explorer > Data Connections > Default Connection > Tables > AspNetUsers
             */
            //TODO: create Model for Games list
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Our game store started by selling local indie games and now we expanded to all games that we love!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Yay, thanks for taking your time to say hi!";

            return View();
        }
    }
}