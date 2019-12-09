using System;
using System.ComponentModel.DataAnnotations;
using Tacovela.MVC.Core.Languages;

namespace Tacovela.MVC.Models.Authentication
{
    public class LoginUserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Es necesario un email valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Es necesario un teléfono valido.")]

        public int Type { get; set; }

        public string ImageProfile { get; set; }

        public string Token { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }

    }
}
