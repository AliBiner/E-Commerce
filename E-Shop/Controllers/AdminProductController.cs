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


namespace E_Shop.Controllers
{
    public class AdminProductController : Controller
    {
        // GET: AdminProduct
        ProductRepository productRepository = new ProductRepository();
        DataContext db = new DataContext();

        public ActionResult Index(int sayfa=1)
        {
            return View(productRepository.List().ToPagedList(sayfa,3));
        }

        public ActionResult Create() 
        {
            List<SelectListItem> deger1 = (from i in db.Categories.ToList()
                                           select new SelectListItem()
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.ctgr = deger1;
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Product data, HttpPostedFileBase File)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }
            
                string path = Path.Combine("~/Content/Image/" + File.FileName);
                File.SaveAs(Server.MapPath(path));
                data.Image = File.FileName.ToString();
                productRepository.Insert(data);
                return RedirectToAction("Index");
            
            
        }

        public ActionResult Delete(int id) 
        {

            productRepository.Delete(productRepository.GetById(id));
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id) 
        {
            List<SelectListItem> deger2 = (from i in db.Categories.ToList()
                                           select new SelectListItem()
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.ctgr2 = deger2;
            return View(productRepository.GetById(id));
         
        }
        
        [HttpPost]

        public ActionResult Update(Product p,  HttpPostedFileBase File)
        {
            var update = productRepository.GetById(p.Id);
            if (!ModelState.IsValid)
            {

                if (File == null)
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

                else
                {

                    update.Name = p.Name;
                    update.Description = p.Description;
                    update.IsApproved = p.IsApproved;
                    update.Popular = p.Popular;
                    update.Price = p.Price;
                    update.Stock = p.Stock;
                    update.Image = File.FileName.ToString();
                    update.CategoryId = p.CategoryId;
                    productRepository.Update(update);
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Hata Oluştu.");
            return View(update);
            
        }

    }
}