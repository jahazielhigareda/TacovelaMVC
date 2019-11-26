using System;
using System.ComponentModel.DataAnnotations;

namespace Tacovela.MVC.Models.Authentication
{
    public class ResetPasswordViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La Re-Contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
