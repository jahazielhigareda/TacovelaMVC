using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Order;

namespace Tacovela.MVC.Models.Taxa
{
    public class TaxaOrderViewModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid TaxaId { get; set; }
        //public OrderViewModel Order { get; set; }
        public TaxaViewModel Taxa { get; set; }

    }
}
