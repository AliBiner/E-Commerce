using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DataContext db = new DataContext();
        public ActionResult Index(decimal? Total)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == username);
                var model = db.Carts.Where(x => x.UserId == user.Id).ToList();
                var kid = db.Carts.FirstOrDefault(x => x.UserId == user.Id);
                if (model!=null)
                {
                    if (kid==null)
                    {
                        ViewBag.Total = "Sepetinizde Ürün Bulunmamaktadır.";
                    }
                    else if (kid != null)
                    {
                        Total = db.Carts.Where(x => x.UserId == kid.UserId).Sum(x => x.Product.Price * x.Quantity);
                        ViewBag.Total = "Toplam Tutar=" + Total + " TL";
                    }
                    return View(model);
                }
                
            }
            return HttpNotFound();
        }


        
        public ActionResult AddCart(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var model = db.Users.FirstOrDefault(x => x.Email == username);
                var prdct = db.Products.Find(id);
                var crt = db.Carts.FirstOrDefault(x => x.UserId == model.Id && x.ProductId == id);
                if (model!=null)
                {
                    if (crt != null)
                    {
                        crt.Quantity++;
                        crt.Price = prdct.Price * crt.Quantity;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Cart");
                    }

                    var s = new Cart
                    {
                        UserId = model.Id,
                        ProductId = prdct.Id,
                        Quantity = 1,
                        Price = prdct.Price,
                        Date = DateTime.Now
                    };
                    db.Carts.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index","Cart");
                }

            }

            return View();
        }

        public ActionResult TotalCount(int? count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var model = db.Users.FirstOrDefault(x => x.Email == username);
                count = db.Carts.Where(x => x.UserId == model.Id).Count();
                ViewBag.count = count;
                if (count==0)
                {
                    ViewBag.count = "";
                }
            }
            return PartialView();
        }

        public void DinamicTotal(int id, int total)
        {
            var model = db.Carts.Find(id);
            model.Quantity = total;
            model.Price = model.Price * model.Quantity;
            db.SaveChanges();
        }

        public ActionResult Reduce(int id)
        {
            var model = db.Carts.Find(id);
            if (model.Quantity==1)
            {
                db.Carts.Remove(model);
                db.SaveChanges();
            }
            model.Quantity--;
            model.Price = model.Price * model.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Increase(int id)
        {
            var model = db.Carts.Find(id);
       
            model.Quantity++;
            model.Price = model.Price * model.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var sil = db.Carts.Find(id);
            db.Carts.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult DeleteRange(int id)
        {
            var sil = db.Carts.Find(id);
            db.Carts.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}