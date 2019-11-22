using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Refit;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Common;
using Tacovela.MVC.Models.Ingredient;

namespace Tacovela.MVC.Controllers
{
    public class IngredientController : BaseController
    {
        public IngredientController(IOptions<Api> enforcerApi) : base(enforcerApi) { }

        public IActionResult Index()
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var model = apiService.IngredientList(new IngredientViewModel()).Result.Data;

            return View(model);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}