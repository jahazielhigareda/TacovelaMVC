using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.User
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "El Nombres (s) es obligatorio.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El Apellidos es obligatorio.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Es necesario un email valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Es necesario un teléfono valido.")]
        public string PhoneNumber { get; set; }
    }
}
