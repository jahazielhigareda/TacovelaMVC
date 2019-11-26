using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

        public List<ProductViewModel> Products { get; set; }
    }
}
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

        public List<ProductViewModel> Products { get; set; }
    }
}
