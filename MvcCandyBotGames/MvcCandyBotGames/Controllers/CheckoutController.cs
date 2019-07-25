using System;
using System.Linq;
using System.Web.Mvc;
using MvcCandyBotGames.Models;

namespace MvcCandyBotGames.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private CandyBotGamesEntities storeDB = new CandyBotGamesEntities();
        const string PromoCode = "FREE";

        //
        // GET: /Checkout/AddressAndPayment

        public ActionResult AddressAndPayment()
        {
            //user doesn't exist
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            var customer = new Customer();
            TryUpdateModel(order);
            TryUpdateModel(customer);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else if(storeDB.Customers.Find(User.Identity.Name) == null) //user not created yet
                {
                    customer.Username = User.Identity.Name;
                    customer.Email = User.Identity.Name;
                    order.Username = customer.Username;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    storeDB.Customers.Add(customer);
                    storeDB.Orders.Add(order);                    
                    storeDB.SaveChanges();

                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderID });
                }
                else //user already exist
                {
                    customer = storeDB.Customers.Find(User.Identity.Name);
                    order.Username = customer.Username;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();

                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderID });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderID == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
