using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkProject.Models;


namespace EntityFrameworkProject.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(string search="",string SortColumn="ProductName",string IconClass="fa-sort-desc",int PageNo=1)
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            ViewBag.search = search;
            List<Product> products = db.Products.Where(temp=>temp.ProductName.Contains(search)).ToList();
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if(ViewBag.SortColumn=="ProductID")
            {
                if(ViewBag.IconClass=="fa-sort-asc")
                                    products = products.OrderBy(temp=>temp.ProductID).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductID).ToList();

            }
            if (ViewBag.SortColumn == "ProductName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.ProductName).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductName).ToList();

            }
            if (ViewBag.SortColumn == "Price")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Price).ToList();
                else
                    products = products.OrderByDescending(temp => temp.Price).ToList();

            }
            if (ViewBag.SortColumn == "CategoryID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Category.CategoryName).ToList();
                else
                    products = products.OrderByDescending(temp => temp.Category.CategoryName).ToList();

            }
            if (ViewBag.SortColumn == "BrandID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Brand.BrandName).ToList();
                else
                    products = products.OrderByDescending(temp => temp.Brand.BrandName).ToList();

            }
            int NoOfRecordPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfRecordsToSkip).Take(NoOfRecordPerPage).ToList();
            return View(products);

        }

        public ActionResult Details(long id)
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            Product products = db.Products.Where(temp => temp.ProductID==id).FirstOrDefault();
            return View(products);
        }
        public ActionResult Create()
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brand = db.Brands.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            if(Request.Files.Count>=1)
            {
                var file = Request.Files[0];
                var imgByte = new byte[file.ContentLength - 1];
                file.InputStream.Read(imgByte, 0, file.ContentLength);
                var base64string = Convert.ToBase64String(imgByte, 0, imgByte.Length);
                p.Photo = base64string;
            }
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(long id)
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brand = db.Brands.ToList();
            Product existproducts = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(existproducts);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            Product existproducts = db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();
            existproducts.ProductName = p.ProductName;
            existproducts.Price = p.Price;
            existproducts.CategoryID = p.CategoryID;
            existproducts.BrandID = p.BrandID;
            existproducts.Active = p.Active;
            existproducts.DateOfPurchase = p.DateOfPurchase;
            db.SaveChanges();
            return RedirectToAction("Index","Products");
        }
        public ActionResult Delete(long id)
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            Product existproducts = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(existproducts);
        }
        [HttpPost]
        public ActionResult Delete(long id,Product p)
        {
            EFWorkDBEntities db = new EFWorkDBEntities();
            Product existproducts = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            db.Products.Remove(existproducts);
            db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
}