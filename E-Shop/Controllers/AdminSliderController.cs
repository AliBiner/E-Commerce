using BusinessLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EntityLayer.Entities;
using System.IO;

namespace E_Shop.Controllers
{
    public class AdminSliderController : Controller
    {
        SliderRepository sliderRepository = new SliderRepository();
        // GET: AdminSlider
        public ActionResult Index(int sayfa = 1)
        {
            return View(sliderRepository.List().ToPagedList(sayfa, 3));
        }

        public ActionResult CreateSlider()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateSlider(Slider data, HttpPostedFileBase File)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }

            string path = Path.Combine("~/Content/Image/" + File.FileName);
            File.SaveAs(Server.MapPath(path));
            data.SliderImage = File.FileName.ToString();
            sliderRepository.Insert(data);
            return RedirectToAction("Index", "AdminSlider");


        }

        public ActionResult UpdateSlider(int id)
        {

            return View(sliderRepository.GetById(id));

        }

        [HttpPost]

        public ActionResult UpdateSlider(Slider p, HttpPostedFileBase File)
        {
            var update = sliderRepository.GetById(p.Id);
            if (ModelState.IsValid)
            {

                if (File == null)
                {

                    update.Header = p.Header;
                    update.Description = p.Description;
                    sliderRepository.Update(update);
                    return RedirectToAction("Index");
                }

                else
                {

                    update.Header = p.Header;
                    update.Description = p.Description;
                    update.SliderImage = File.FileName.ToString();
                    sliderRepository.Update(update);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Hata Oluştu.");
            return View(update);
        }

        private ActionResult DeleteSlider(int id)
        {
            sliderRepository.Delete(sliderRepository.GetById(id));
            return RedirectToAction("Index");
        }
    }
}