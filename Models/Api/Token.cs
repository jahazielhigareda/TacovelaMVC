using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Api
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string ImageProfile { get; set; }
    }
}
