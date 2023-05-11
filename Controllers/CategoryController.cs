using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkProject.Models;

namespace EntityFrameworkProject.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category/Index
        public ActionResult Index()
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            List<Category>categories= db.Categories.ToList();

            return View(categories);
        }
    }
}