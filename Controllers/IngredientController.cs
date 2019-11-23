using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Ingredient;

namespace Tacovela.MVC.Controllers
{
    public class IngredientController : BaseController
    {
        public IngredientController(IOptions<Api> enforcerApi) : base(enforcerApi) { }

        public async Task<IActionResult> Index()
        {
            var model = new List<IngredientViewModel>();
            var apiService = RestServiceExtension<IIngredientAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var resultService = await apiService.GetList(new IngredientViewModel());

            model = AwaitPaginationResult<List<IngredientViewModel>>(model, resultService);
            return View(model);
        }

        public async Task<IActionResult> NewEdit(Guid? id)
        {
            var model = new IngredientViewModel();
            ViewData["SubTitle"] = "Nuevo";
            ViewData["SubTitleDescription"] = "Para crear un nuevo componentes debera llenar el siguiente formulario.";

            if (id != null && id != Guid.Empty)
            {
                var apiService = RestServiceExtension<IIngredientAPI>.For(_enforcerApi.Url, GetUserSession().Token);

                var resultService = await apiService.GetById(id.Value);

                model = AwaitDataResult<IngredientViewModel>(model, resultService);
                ViewData["SubTitle"] = "Editar";
                ViewData["SubTitleDescription"] = "Para editar el componentes debera llenar el siguiente formulario.";
            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> NewEdit(IngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null || model.Id == Guid.Empty)
                {
                    var apiService = RestServiceExtension<IIngredientAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                    var resultService = await apiService.Create(model);
                    BasicResponse<IngredientViewModel>(model, resultService, true);
                    ViewData["SubTitle"] = "Nuevo";
                    ViewData["SubTitleDescription"] = "Para crear un nuevo componentes debera llenar el siguiente formulario.";
                }
                else
                {
                    var apiService = RestServiceExtension<IIngredientAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                    var resultService = await apiService.Edit(model);
                    BasicResponse<IngredientViewModel>(model, resultService);
                    ViewData["SubTitle"] = "Editar";
                    ViewData["SubTitleDescription"] = "Para editar el componentes debera llenar el siguiente formulario.";
                }
            }
            model = new IngredientViewModel { Id = model.Id };
            return View(model);
        }
    }
}