using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Common
{
    public class FilterViewModel<T> : BaseFilterViewModel
    {
        public T Filter { get; set; }
    }
}
