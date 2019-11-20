using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Common
{
    public class BaseFilterViewModel
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public bool ApplyFilter { get; set; } = false;
    }
}
