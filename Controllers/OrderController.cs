using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Category;
using Tacovela.MVC.Models.Order;
using Tacovela.MVC.Models.Product;

namespace Tacovela.MVC.Controllers
{
    public class OrderController : CommonController
    {
        public OrderController(IOptions<Api> enforcerApi) : base(enforcerApi)
        {
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var model = new OrderViewModel();

            var apiService = RestService.For<IAPI>(_enforcerApi.Url);
            var resultService = await apiService.GetCategoryListByAnonymous();
            model.Categories = GetData<List<CategoryViewModel>>(resultService);



            return View(model);
        }

        public async Task<IActionResult> List()
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            var model = apiService.GetOrder().Result.Data;

            return View(model);
        }

        public IActionResult ChangeStatus(Guid orderId, int status)
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            var resultService = apiService.ChangeOrderStatus(orderId, status);

            ModelStateMessage<ApiResponse<BasicResponse>>(resultService, true);

            return RedirectToAction("Index");
        }
    }
}