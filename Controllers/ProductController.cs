using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Refit;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;

namespace Tacovela.MVC.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IOptions<Api> enforcerApi) : base(enforcerApi) { }

        public IActionResult Index()
        {
            var apiService = RestService.For<IAPI>(_enforcerApi.Url);
            var model = apiService.ProductList().Result.Data;

            return View(model);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}