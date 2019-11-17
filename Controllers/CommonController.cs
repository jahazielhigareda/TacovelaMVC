using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tacovela.MVC.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Controllers
{
    public class CommonController : Controller
    {
        protected readonly Api _enforcerApi;
        public CommonController(IOptions<Api> enforcerApi)
        {
            _enforcerApi = enforcerApi.Value;
        }

        protected void HandleMessages(string[] errors, string messageType)
        {
            foreach (var erro in errors)
            {
                ModelState.AddModelError(messageType, erro);
            }
        }
    }
}
