using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El Nombres (s) es obligatorio.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El Apellidos es obligatorio.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Es necesario un email valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Es necesario un teléfono valido.")]
        public string PhoneNumber { get; set; }

        public int Type { get; set; }
        public string ImageProfile { get; set; } = "//placehold.it/60";

        //public UserAddressViewModel Address { get; set; }
        //public string Street { get; set; }
        //public string Number { get; set; }
        //public string PostalCode { get; set; }
        //public string City { get; set; }
        //public string SubCity { get; set; }
        //public string State { get; set; }
    }
}
