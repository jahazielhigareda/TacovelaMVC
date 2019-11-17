using Microsoft.AspNetCore.Http;
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
            HttpContext.Session.SetString("Token", string.Empty);
            return RedirectToAction("Index", "Authentication");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
