using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoneStore.Models;
using System.Data.Entity;

namespace StoneStore.Controllers
{
    public class NavController : Controller
    {
        private DbSet<Stone> repository = new db_StoneEntities().Stones;

        //public NavController()
        //{
        //    var repository = new db_StoneEntities().Stones;
        //}
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Select(x => x.Category).Distinct().OrderBy(x => x);
            return PartialView(categories);
        }
    }
}