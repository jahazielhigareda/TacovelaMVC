using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Category;
using Tacovela.MVC.Models.Product;

namespace Tacovela.MVC.Models.Order
{
    public class ShopViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public List<OrderProductViewModel> Products { get; set; } = new List<OrderProductViewModel>();

        public Guid? FilterCategory { get; set; }
        public Guid UserAddress { get; set; }
    }

    public class OrderViewModel
    {
        public List<OrderProductViewModel> Products { get; set; } = new List<OrderProductViewModel>();

        public Guid UserAddress { get; set; }
        public DateTime Date { get; set; }
    }

    public class OrderProductViewModel : ProductViewModel {
        public int Number { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public Guid? ProductId { get; set; }
    }
}
