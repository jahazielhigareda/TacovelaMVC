using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Api
{
    public class ResultViewModelBase
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
