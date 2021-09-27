using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPanel_.Model.ComplexType
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen kullanıcı kodunu adresinizi giriniz.")]
        [Display(Name = "Kullanıcı Kodu")]
        public string Username { get; set; }
    }
}
