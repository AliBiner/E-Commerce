using BusinessLayer.Concreate;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminCategoryController : Controller
    {

        // GET: AdminCategory
        DataContext db = new DataContext();
        CategoryRepository categoryRepository = new CategoryRepository();
        public ActionResult Index()
        {
            return View(categoryRepository.List());
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]

       
        public ActionResult Create(Category p)
        {
            if (ModelState.IsValid)
            {
                p.Status = true;
                
                categoryRepository.Insert(p);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Bir Hata Oluştu");
            return View();

        }

        public ActionResult Delete(int id) {
            var kategori = db.Categories.Where(x => x.Id == id).FirstOrDefault();
            kategori.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      

        public ActionResult Update(int id) {
            return View(categoryRepository.GetById(id));
        }

        
        [HttpPost]

        public ActionResult Update(Category p) 
        {
            if (ModelState.IsValid)
            {
                var update = categoryRepository.GetById(p.Id);
                update.Name = p.Name;
                update.Description = p.Description;
                categoryRepository.Update(update);
                return RedirectToAction("Index");

            }

            ModelState.AddModelError("", "Bir Hata Oluştu");
            return View();

        }




    }
}