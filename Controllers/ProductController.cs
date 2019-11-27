using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Enums;
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
            var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var model = apiService.ProductList().Result.Data;

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
                if(!productIngredients.Select(p => p.IngredientId).Contains(ingredient.Id))
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

        //public IActionResult Delete(Guid id)
        public IActionResult Delete()
        {
            //var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            //var model = apiService.ProductList().Result.Data;

            //return View(model.FirstOrDefault());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var model = apiService.ProductList(id).Result.Data.FirstOrDefault();

            model.IsActive = false;

            var resultService = await apiService.UpdateProduct(model, null);

            return RedirectToAction("Index");
        }
    }
}