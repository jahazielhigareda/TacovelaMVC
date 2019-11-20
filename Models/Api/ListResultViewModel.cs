using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Api
{
    public class ListResultViewModel<T> : ResultViewModelBase
    {
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
