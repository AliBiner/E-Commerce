using BusinessLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        SliderRepository sliderRepository = new SliderRepository();
        public PartialViewResult SliderList()
        {
            return PartialView(sliderRepository.List()); ;
        }
    }
}