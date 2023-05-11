using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkProject.Models;


namespace EntityFrameworkProject.Controllers
{
    public class BrandsController : Controller
    {
        // GET: Brands
        public ActionResult Index()
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            List<Brand> brands = db.Brands.ToList();

            return View(brands);

        }
    }
}