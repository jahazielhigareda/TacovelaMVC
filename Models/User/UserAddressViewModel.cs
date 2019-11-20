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
        [MinLength(2)]
        [MaxLength(100)]
        public string Street { get; set; }

        [Required(ErrorMessage = "La numero es obligatorio.")]
        [MinLength(2)]
        [MaxLength(100)]
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
