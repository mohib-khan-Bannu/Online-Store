using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Soft_Product.Models.LoginSign
{
    public class LoginSingUp
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile")]

        public long Moblie { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter ConfrirmPassword")]
        public string ConfrirmPassword { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
        public bool IsAdmin { get; set; }
    }
}
