using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Api
{
    public class LoginResponse : BasicResponse<TokenResponse>
    {
        public object ErrorCode { get; set; } = 0;
    }
}
