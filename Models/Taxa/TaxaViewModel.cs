using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Taxa
{
    public class TaxaViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
    }
}
