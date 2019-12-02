using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Ingredient;
using Tacovela.MVC.Models.Product;

namespace Tacovela.MVC.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IOptions<Api> enforcerApi) : base(enforcerApi) { }

        public IActionResult Index()
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var model = apiService.ProductList().Result.Data.Where(w => w.IsActive).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new ProductViewModel();
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var ingredients = apiService.IngredientList(new IngredientViewModel()).Result.Data;
            var categories = apiService.GetCategory().Result.Data;

            categories.Add(new Models.Category.CategoryViewModel());
            ViewBag.Categories = new SelectList(categories.Select(p => new { p.Id, p.Name }), "Id", "Name");
            model.ProductIngredients = new List<ProductIngredientViewModel>();

            foreach (var ingredient in ingredients)
            {
                model.ProductIngredients.Add(new ProductIngredientViewModel()
                {
                    Ingredient = ingredient,
                    IngredientId = ingredient.Id,
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            ApiResponse<BasicResponse> resultService;
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            if (product.Image != null)
            {
                using (var stream = product.Image.OpenReadStream())
                {
                    resultService = await apiService.CreateProduct(product, new StreamPart(stream, product.Image.FileName));
                }
            }
            else
            {
                product.UrlImage = product.UrlImage ?? "";
                resultService = await apiService.CreateProduct(product, null);
            }

            ModelStateMessage<ApiResponse<BasicResponse>>(resultService, true);

            var categories = apiService.GetCategory().Result.Data;
            ViewBag.Categories = new SelectList(categories.Select(p => new { p.Id, p.Name }), "Id", "Name");

            var ingredients = apiService.IngredientList(new IngredientViewModel()).Result.Data;
            var model = new ProductViewModel() { ProductIngredients = new List<ProductIngredientViewModel>() };

            foreach (var ingredient in ingredients)
            {
                model.ProductIngredients.Add(new ProductIngredientViewModel()
                {
                    Ingredient = ingredient,
                    IngredientId = ingredient.Id,
                });
            }

            return View(model);
        }

        public IActionResult Edit(Guid id)
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            var categories = apiService.GetCategory().Result.Data;
            ViewBag.Categories = new SelectList(categories.Select(p => new { p.Id, p.Name }), "Id", "Name");

            var model = apiService.ProductList(id).Result.Data.FirstOrDefault();

            var productIngredients = apiService.GetIngredients(id).Result.Data;
            var ingredients = apiService.IngredientList(new IngredientViewModel()).Result.Data;

            model.ProductIngredients = productIngredients;
            foreach (var ingredient in ingredients)
            {
                if (!productIngredients.Select(p => p.IngredientId).Contains(ingredient.Id))
                {
                    model.ProductIngredients.Add(new ProductIngredientViewModel()
                    {
                        Ingredient = ingredient,
                        IngredientId = ingredient.Id,
                    });
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {
            ApiResponse<BasicResponse> resultService;
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            if (product.Image != null)
            {
                using (var stream = product.Image.OpenReadStream())
                {
                    resultService = await apiService.UpdateProduct(product, new StreamPart(stream, product.Image.FileName));
                }
            }
            else
            {
                product.UrlImage = product.UrlImage ?? "";
                resultService = await apiService.UpdateProduct(product, null);
            }

            ModelStateMessage<ApiResponse<BasicResponse>>(resultService);

            var categories = apiService.GetCategory().Result.Data;
            ViewBag.Categories = new SelectList(categories.Select(p => new { p.Id, p.Name }), "Id", "Name");

            var model = apiService.ProductList(product.Id).Result.Data.FirstOrDefault();
            var productIngredients = apiService.GetIngredients(product.Id).Result.Data;
            var ingredients = apiService.IngredientList(new IngredientViewModel()).Result.Data;

            model.ProductIngredients = productIngredients;
            foreach (var ingredient in ingredients)
            {
                if (!productIngredients.Select(p => p.IngredientId).Contains(ingredient.Id))
                {
                    model.ProductIngredients.Add(new ProductIngredientViewModel()
                    {
                        Ingredient = ingredient,
                        IngredientId = ingredient.Id,
                    });
                }
            }

            return View(model);
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var model = apiService.ProductList(id).Result.Data.FirstOrDefault();

            model.IsActive = false;

            var resultService = await apiService.UpdateProduct(model, null);

            TempDataMessage<BasicResponse>(resultService, true);

            return RedirectToAction("Index");
        }
    }
}