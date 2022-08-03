using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EntityLayer.Entities
{
    public class Picture
    {
        public int Id { get; set; }

        public string PathName { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
