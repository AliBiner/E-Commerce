using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class User
    {
        public int Id { get; set; }

        //
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = " Max. 50 Karakter Olamalıdır")]
        public string Name { get; set; }

        //
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = " Max. 50 Karakter Olamalıdır")]
        public string SurName { get; set; }

        //
        //[Required(ErrorMessage = "Boş Geçilemez")]
        //[Display(Name = "E-Mail")]
        //[StringLength(50, ErrorMessage = " Max. 50 Karakter Olamalıdır")]
        //[EmailAddress(ErrorMessage = "E-Mail Formatı Şeklinde Giriniz.")]
        public string Email { get; set; }

        //
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(50, ErrorMessage = " Max. 50 Karakter Olamalıdır")]
        public string UserName { get; set; }

        //
        //[Required(ErrorMessage = "Boş Geçilemez")]
        //[Display(Name = "Şifre")]
        //[StringLength(50, ErrorMessage = " Max. 50 Karakter Olamalıdır")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //
        //[Required(ErrorMessage = "Boş Geçilemez")]
        //[Display(Name = "Şifre Tekrar")]
        //[StringLength(50, ErrorMessage = " Max. 50 Karakter Olamalıdır")]
        //[DataType(DataType.Password)]
        //[Compare("Password",ErrorMessage ="Şifreler Uyuşmuyor")]
        public string RePassword { get; set; }

        //
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Rol")]
        [StringLength(50, ErrorMessage = " Max. 50 Karakter Olamalıdır")]
        public string Role { get; set; }
    }
}
