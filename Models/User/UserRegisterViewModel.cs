using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.User
{
    public class UserRegisterViewModel : UserLoginViewModel
    {
        [Required(ErrorMessage = "El Nombres (s) es obligatorio.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El Apellidos es obligatorio.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "La Re-Contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public int Type { get; set; } = 1;
    }
}
