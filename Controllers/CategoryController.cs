using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Category;

namespace Tacovela.MVC.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IOptions<Api> enforcerApi) : base(enforcerApi) { }

        public IActionResult Index()
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var categories = apiService.GetCategory().Result.Data.Where(w => w.IsActive).ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel category)
        {
            ApiResponse<BasicResponse> resultService;
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            if (category.Image != null)
            {
                using (var stream = category.Image.OpenReadStream())
                {
                    resultService = await apiService.CreateCategory(category, new StreamPart(stream, category.Image.FileName));
                }
            }
            else
            {
                category.ImageUrl = category.ImageUrl ?? "";
                resultService = await apiService.CreateCategory(category, null);
            }

            TempDataMessage<ApiResponse<BasicResponse>>(resultService, true);

            return View(new CategoryViewModel());
        }

        public IActionResult Edit(Guid id)
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            var category = apiService.GetCategory(id).Result.Data;

            return View(category.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel category)
        {
            ApiResponse<BasicResponse> resultService;
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            if (category.Image != null)
            {
                using (var stream = category.Image.OpenReadStream())
                {
                    resultService = await apiService.UpdateCategory(category, new StreamPart(stream, category.Image.FileName));
                }
            }
            else
            {
                category.ImageUrl = category.ImageUrl ?? "";
                resultService = await apiService.UpdateCategory(category, null);
            }

            ModelStateMessage<ApiResponse<BasicResponse>>(resultService);

            var model = apiService.GetCategory(category.Id).Result.Data;

            return View(model.FirstOrDefault());
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new CategoryViewModel() { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var model = apiService.GetCategory(id).Result.Data.FirstOrDefault();

            model.IsActive = false;

            var resultService = await apiService.UpdateCategory(model, null);

            TempDataMessage<BasicResponse>(resultService, true);

            return RedirectToAction("Index");
        }
    }
}