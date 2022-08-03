using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Slider
    {
        public int Id { get; set; }

        //
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Başlık")]
        public string Header { get; set; }

        //
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        public string SliderImage { get; set; }

    }
}
