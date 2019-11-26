using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tacovela.MVC.Models.Ingredient
{
    public class IngredientViewModel
    {
        [Key]
        [JsonProperty(Required = Required.AllowNull)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio."), MinLength(2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El Valor es obligatorio.")]
        [Range(0, 5000, ErrorMessage = "El valor tiene que ser entre 0 a 5000")]
        public decimal? Value { get; set; }
    }
}
