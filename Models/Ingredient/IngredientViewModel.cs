using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Ingredient
{
    public class IngredientViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Range(0, 5000)]
        public decimal? Value { get; set; }
    }
}
