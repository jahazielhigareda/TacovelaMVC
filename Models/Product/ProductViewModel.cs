using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Product
{
    public class ProductViewModel
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [Range(0, 5000)]
        public decimal Value { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public string Description { get; set; }
    }
}
