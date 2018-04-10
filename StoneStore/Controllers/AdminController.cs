using StoneStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoneStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private DbSet<Stone> repository = new db_StoneEntities().Stones;
        
        public ViewResult Index()
        {
            return View(repository);
        }
        public ViewResult Edit(int productId)
        {
            Stone product = repository.FirstOrDefault(p => p.Id == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Stone product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                //if (image != null)
                //{
                //    product.ImageData = new byte[image.ContentLength];
                //    product.ImageMimeType = image.ContentType;
                //    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                //}
                dbMethods.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Stone());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Stone deletedProduct = dbMethods.Deletestone(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}