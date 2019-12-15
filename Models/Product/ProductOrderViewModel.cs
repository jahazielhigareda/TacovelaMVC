using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tacovela.MVC.Models.Ingredient;

namespace Tacovela.MVC.Models.Product
{
    public class ProductOrderViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; }
        public List<ProductOrderIngredientViewModel> Ingredients { get; set; }
    }
}
