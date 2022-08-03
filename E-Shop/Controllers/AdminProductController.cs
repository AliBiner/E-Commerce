using BusinessLayer.Concreate;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System;

namespace E_Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {

        // GET: AdminProduct
        ProductRepository productRepository = new ProductRepository();
        PictureRepository pictureRepository = new PictureRepository();
        DataContext db = new DataContext();


        public ActionResult Index(int sayfa = 1)
        {
            return View(productRepository.List().ToPagedList(sayfa, 3));
        }

        public ActionResult Create()
        {
            List<SelectListItem> deger1 = (from i in db.Categories.Where(x => x.Status == true).ToList()
                                           select new SelectListItem()
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.ctgr = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product data, List<HttpPostedFileBase> File)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in File)
                {
                    string path = Path.Combine("~/Content/Image/" + item.FileName);
                    item.SaveAs(Server.MapPath(path));
                    data.Pictures.Add(new Picture { PathName = item.FileName });

                }
                db.Products.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(data);
        }


        public ActionResult PictureList(int id)
        {
            
            List<SelectListItem> deger1 = (from i in db.Pictures.Where(x=>x.ProductId==id).ToList()
                                           select new SelectListItem()
                                           {
                                               Text = i.PathName,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.PictureList = deger1;

            
            return View();
        }









        public ActionResult Update(int id)
        {
            var update = db.Products.Where(x => x.Id == id).FirstOrDefault() ;
            List<SelectListItem> deger2 = (from i in db.Categories.Where(x => x.Status == true).ToList()
                                           select new SelectListItem()
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.ctgr2 = deger2;
            return View(productRepository.GetById(id));

        }

        [HttpPost]

        public ActionResult Update(Product p, List<HttpPostedFileBase> File)
        {
            var update = productRepository.GetById(p.Id);
            
            if (ModelState.IsValid)
            {

               

                    update.Name = p.Name;
                    update.Description = p.Description;
                    update.IsApproved = p.IsApproved;
                    update.Popular = p.Popular;
                    update.Price = p.Price;
                    update.Stock = p.Stock;
                    
                    
                    update.CategoryId = p.CategoryId;
                    productRepository.Update(update);
                    return RedirectToAction("Index");
                

                
            }

            ModelState.AddModelError("", "Hata Oluştu.");
            return View(update);

        }




        public ActionResult CriticalStock()
        {
            var kritik = db.Products.Where(x => x.Stock <= 50).ToList();
            return View(kritik);
        }

        public PartialViewResult StockCount()
        {
            if (User.Identity.IsAuthenticated)
            {
                var count = db.Products.Where(x => x.Stock < 50).Count();
                ViewBag.count = count;
                var azalan = db.Products.Where(x => x.Stock == 50).Count();
                ViewBag.azalan = azalan;
            }
            return PartialView();
        }

    }
}
