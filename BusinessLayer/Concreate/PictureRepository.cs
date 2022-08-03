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
    public class PictureRepository:GenericRepository<Picture>
    {
        DataContext db = new DataContext();
        public List<Picture> PictureCreated()
        {
            return db.Pictures.ToList();
        }
        
    }
}
