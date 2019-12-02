using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Category;
using Tacovela.MVC.Models.Product;
using Tacovela.MVC.Models.User;

namespace Tacovela.MVC.Models.Order
{
    public class ShopViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductOrderViewModel> Products { get; set; } = new List<ProductOrderViewModel>();

        public Guid? FilterCategory { get; set; }
        public Guid UserAddress { get; set; }
    }

    public class OrderViewModel
    {
        public Guid? Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserAddressId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public UserAddressViewModel UserAddress { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductOrderViewModel> Products { get; set; }
        //public List<OrderProductViewModel> Products { get; set; } = new List<OrderProductViewModel>();
    }

    public enum OrderStatus
    {
        InCard,
        Pending,
        InPreparation,
        OnWay,
        Canceled,
        Delivered
    }

    //public class OrderProductViewModel : ProductViewModel {
    //    public int Number { get; set; }
    //    public decimal Cost { get; set; }
    //    public int Quantity { get; set; }
    //    public Guid? ProductId { get; set; }
    //}
}
