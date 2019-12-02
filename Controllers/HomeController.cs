﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using Tacovela.MVC.Core.Enums;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.User;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;

namespace Tacovela.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<Api> enforcerApi) : base(enforcerApi)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            //SetUserSession(null);
            //return RedirectToAction("Index", "Authentication");
            return View();
        }

        public IActionResult SessionLogout()
        {
            SetUserSession(null);
            return RedirectToAction("Index", "Authentication");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var Exeption = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Exeption.Error.Message });
        }
    }
}
