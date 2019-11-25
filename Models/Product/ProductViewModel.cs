using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Ingredient;

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
        public bool IsActive { get; set; }

        public string UrlImage { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public string Description { get; set; }

        public List<ProductIngredientViewModel> Ingredients {get; set;}
    }
}
