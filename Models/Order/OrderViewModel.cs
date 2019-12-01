using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Category;
using Tacovela.MVC.Models.Product;

namespace Tacovela.MVC.Models.Order
{
    public class OrderViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
