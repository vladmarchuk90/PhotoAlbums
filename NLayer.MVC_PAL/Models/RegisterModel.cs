using System;
using System.ComponentModel.DataAnnotations;

 
namespace NLayer.MVC_PAL.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not the same")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}