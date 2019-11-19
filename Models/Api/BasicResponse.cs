using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Api
{
    public class BasicResponse<T>
    {
        public object ErrorCode { get; set; } = 0;
        public bool Success { get; set; }
        public T Data { get; set; }
        public string[] Errors { get; set; }
    }
}
