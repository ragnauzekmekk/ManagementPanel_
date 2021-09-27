using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPanel_.Model.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(5,ErrorMessage ="Ad 5 karakter uzunluğunda olmalıdır.")]
        public string Username { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="Ad 50 karakter uzunluğunda olmalıdır.")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Soyad 50 karakter uzunluğunda olmalıdır.")]
        public string Surname { get; set; }

        [StringLength(50, ErrorMessage = "Ad 50 karakter uzunluğunda olmalıdır.")]
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Ad 50 karakter uzunluğunda olmalıdır.")]
        public string Email { get; set; }


        [Required]
        [StringLength(10, ErrorMessage = "Telefon numaranızı kontrol ediniz.")]
        public string Phone { get; set; }


        public DateTime Date { get; set; }
        public bool Admin { get; set; }
    }
}
