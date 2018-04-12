using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoneStore.Models;
using PagedList;

namespace StoneStore.Controllers
{
    public class HomeController : Controller
    {
        db_StoneEntities db = new db_StoneEntities();
        ProductListViewModel viewModel;
        // GET: Home
        public ViewResult Index(string category, int? pageNumber)
        {
            viewModel = new ProductListViewModel()
            {
                CurrentCategory = category
            };
            return View(db.Stones.ToList().Where(p => category == null || p.Category == category).OrderBy(x => x.Id).ToPagedList(pageNumber ?? 1, 5));
        }
        

        public FileContentResult GetImage(int productId)
        {
            Stone prod = db.Stones.FirstOrDefault(p => p.Id == productId);
            if (prod != null)
            {
                //return File(prod.ImageData, prod.ImageMimeType);
                return null;
            }
            else
            {
                return null;
            }
        }

        public PartialViewResult ProductSummary(Stone product)
        {
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult ProductSummary(string Sizes, string prodName)
        {
            string thisSize = Sizes[0].ToString();
            Stone prod = db.Stones.Where(x => x.Name == prodName && x.Size == thisSize).First();
            return View(prod);
        }
    }
}