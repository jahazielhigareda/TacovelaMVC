using System;
using System.ComponentModel.DataAnnotations;
using Tacovela.MVC.Models.Ingredient;

namespace Tacovela.MVC.Models.Product
{
    public class ProductIngredientViewModel
    {
        [Required]
        public Guid IngredientId { get; set; }

        [Required]
        public bool Addable { get; set; }

        [Required]
        public bool Removable { get; set; }

        public IngredientViewModel Ingredient { get; set; }
    }
}
