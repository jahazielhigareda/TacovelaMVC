using System;
using System.ComponentModel.DataAnnotations;
using Tacovela.MVC.Models.Ingredient;

namespace Tacovela.MVC.Models.Product
{
    public class ProductOrderIngredientViewModel
    {
        [Required]
        public bool Add { get; set; }

        [Required]
        public bool Remove { get; set; }

        public Guid IngredientId { get; set; }
        public IngredientViewModel Ingredient { get; set; }
    }
}
