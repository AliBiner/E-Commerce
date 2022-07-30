using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EntityLayer.Entities;

namespace E_Shop.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        DataContext db = new DataContext();
        public ActionResult Index(int sayfa=1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == username);
                var model = db.Sales.Where(x => x.UserId == user.Id).ToList().ToPagedList(sayfa, 5);
                return View(model);
            }
            
            return HttpNotFound();
        }

        public ActionResult Buy(int id)
        {
            var model = db.Carts.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpPost]

        public ActionResult Buy2(int id)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var model = db.Carts.FirstOrDefault(x => x.Id == id);
                    var satis = new Sales
                    {
                        UserId = model.UserId,
                        ProductId = model.ProductId,
                        Quantity = model.Quantity,
                        Image = model.Image,
                        Price = model.Price,
                        Date = DateTime.Now,
                        
                    };

                    db.Carts.Remove(model);
                    db.Sales.Add(satis);
                    db.SaveChanges();
                    ViewBag.islem = "Satın Alma İşlemi Başarılı Bir Şekilde Gerçekleşmiştir.";
                }
            }
            catch (Exception)
            {

                ViewBag.islem = "Satın Alma İşlemi Başarısız";
            }
            return View("islem");
        }

        public ActionResult BuyAll(decimal? Tutar)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                var user = db.Users.FirstOrDefault(x => x.Email == username);
                var model = db.Carts.Where(x => x.UserId == user.Id).ToList();
                var userid = db.Carts.FirstOrDefault(x => x.UserId == user.Id);
                if (model != null)
                {
                    if (userid == null)
                    {
                        ViewBag.Tutar = "Sepetinizde ürün bulunmamaktadır.";
                    }
                    else if (userid!=null)
                    {
                        Tutar = db.Carts.Where(x => x.UserId == userid.UserId).Sum(x => x.Product.Price * x.Quantity);
                        ViewBag.Tutar = "Toplam Tutar =" + Tutar + " TL";
                    }
                    return View(model);
                }
                return View();
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult BuyAll2()
        {
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(x => x.Email == username);
            var model = db.Carts.Where(x => x.UserId == user.Id).ToList();
            int row = 0;
            foreach (var item in model)
            {
                var satis = new Sales
                {
                    UserId = model[row].UserId,
                    ProductId = model[row].ProductId,
                    Quantity = model[row].Quantity,
                    Price = model[row].Price,
                    Image = model[row].Image,
                    Date = DateTime.Now,
                };
                db.Sales.Add(satis);
                db.SaveChanges();
                row++;
            }
            db.Carts.RemoveRange(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}