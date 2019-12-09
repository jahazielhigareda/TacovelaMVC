using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Order
{
    public class AddProductIngredientViewModel
    {
        public Guid IngredientId { get; set; }
        public int Quantity { get; set; }
    }
}
