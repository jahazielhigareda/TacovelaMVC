using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Authentication;

namespace Tacovela.MVC.Models.User
{
    public class ChangePasswordViewModel : ResetPasswordViewModel
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
    }
}
