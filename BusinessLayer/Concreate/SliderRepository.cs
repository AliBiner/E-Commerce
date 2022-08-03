using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate
{
    public class SliderRepository : GenericRepository<Slider>
    {
        DataContext db = new DataContext();
        public List<Slider> SliderList()
        {
            return db.Sliders.ToList();
        }
    }
}
