using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tacovela.MVC.Models.Product;

namespace Tacovela.MVC.Models.Category
{
    public class CategoryViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile Image { get; set; }

        public bool IsActive { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}