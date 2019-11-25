using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Api
{
    public class BasicResponse<T> : ResultViewModelBase
    {
        public object ErrorCode { get; set; } = 0;
        public T Data { get; set; }
    }

    public class BasicResponse : ResultViewModelBase
    {
        public object ErrorCode { get; set; } = 0;
    }

    public class ListResultViewModel<T> : ResultViewModelBase
    {
        public T Data { get; set; }
        public int Count { get; set; }
    }
}
