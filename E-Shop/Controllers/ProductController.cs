using BusinessLayer.Concreate;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Shop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProductRepository productRepository = new ProductRepository();
        DataContext db = new DataContext();
        public PartialViewResult PopularProduct()
        {
            var product = productRepository.GetPopularProduct();
            ViewBag.popular = product;
            return PartialView();
        }
        [Route("product/productdetails/{id}/{name}")]
        public ActionResult ProductDetails(int id)
        {
            var details = productRepository.GetById(id);
            var comment = db.Comments.Where(x => x.ProductId == id).ToList();
            ViewBag.comment = comment;
            return View(details);
        }

        [HttpPost]
        public ActionResult Comment(Comment data)
        {
            if (User.Identity.IsAuthenticated)
            {
               
                db.Comments.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }
    }
}