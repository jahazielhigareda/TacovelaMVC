using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.User
{
    public class UserAddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "La calle es obligatorio.")]
        [MinLength(2, ErrorMessage = "El campo calle debe contener al menos 2 caracter.")]
        [MaxLength(100, ErrorMessage = "El campo calle solo soporta una longitud de caracteres de 100.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "La numero es obligatorio.")]
        [MinLength(1, ErrorMessage = "El campo numero debe contener al menos 1 caracter.")]
        [MaxLength(20, ErrorMessage = "El campo calle solo soporta una longitud de caracteres de 20.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "La codigo postal es obligatorio.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatorio.")]
        [MinLength(2)]
        [MaxLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "La colonia es obligatorio.")]
        [MinLength(2)]
        [MaxLength(100)]
        public string SubCity { get; set; }

        [Required(ErrorMessage = "La estado es obligatorio.")]
        [MinLength(2)]
        [MaxLength(100)]
        public string State { get; set; }
    }
}
