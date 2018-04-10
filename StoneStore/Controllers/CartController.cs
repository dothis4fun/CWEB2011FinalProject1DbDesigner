using StoneStore.Entities;
using StoneStore.Models;
using StoneStore.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoneStore.Controllers
{
    public class CartController : Controller
    {
        private DbSet<Stone> repository = new db_StoneEntities().Stones;
        private EmailOrderProcessor proc;
        public CartController()
        {

        }
        public CartController(EmailOrderProcessor proc)
        {
            this.proc = proc;
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            Stone product = repository.FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                cart.Additem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productID, string returnUrl)
        {
            Stone product = repository.FirstOrDefault(p => p.Id == productID);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                proc.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}