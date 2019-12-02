using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Enums;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Category;
using Tacovela.MVC.Models.Order;
using Tacovela.MVC.Models.Product;
using Tacovela.MVC.Models.User;

namespace Tacovela.MVC.Controllers
{
    public class OrderController : CommonController
    {
        public OrderController(IOptions<Api> enforcerApi) : base(enforcerApi)
        {
        }

        public async Task<IActionResult> Index(Guid? id)
        {
            var model = new ShopViewModel();

            var apiService = RestService.For<IAPI>(_enforcerApi.Url);
            var resultService = await apiService.GetCategoryListByAnonymous();
            model.Categories = GetData<List<CategoryViewModel>>(resultService);

            var listShop = GetCardSession();

            if (listShop != null && listShop.Any())
            {
                var list = model.Categories.Select(s => s.Products).ToList();
                var lisProduct = new List<ProductViewModel>();
                foreach (var item in list)
                {
                    lisProduct.AddRange(item);
                }

                var productWasAdd = lisProduct.Where(w => listShop.Select(s => s.Id).Contains(w.Id)).Select(s => new ProductOrderViewModel
                {
                    Product = new ProductViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        //Image = item.FirstOrDefault().Image,
                        UrlImage = s.UrlImage,
                        Value = s.Value * listShop.Where(w => w.Id == s.Id).Count(),
                    },
                    Quantity = listShop.Where(w => w.Id == s.Id).Count()
                }).ToList();
                model.Products = productWasAdd;
            }
            //var resultServiceProduct = await apiService.GetProdutByIdCategoryByAnonymous(id);
            //model.Products = GetData<List<ProductViewModel>>(resultServiceProduct);
            model.FilterCategory = id;

            return View(model);
        }

        public async Task<IActionResult> List()
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var user = GetUserSession();
            
            var model = apiService.GetOrder(user.Type == (int)UserType.Client ? user.Id : (Guid?)null).Result.Data;
            return View(model);
        }
        public async Task<IActionResult> EndOrder()
        {
            var listShop = GetCardSession();
            if (listShop == null || !listShop.Any())
            {
                TempDataMessages(new string[] { "No ha agregado nada al pedido." }, TagHelperStatusEnum.Error.ToString());
                return RedirectToAction("Index");
            }

            try
            {
                var model = new OrderViewModel();

                #region Get Id Address

                var userId = GetUserSession().Id;

                var apiServiceAddress = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                var resultAddress = await apiServiceAddress.GetAddressByIdUser(userId);
                var address = GetData<UserAddressViewModel>(resultAddress);

                #endregion

                #region List Product


                var apiServiceCategories = RestService.For<IAPI>(_enforcerApi.Url);
                var resultServiceCategories = await apiServiceCategories.GetCategoryListByAnonymous();
                var categories = GetData<List<CategoryViewModel>>(resultServiceCategories);

                var list = categories.Select(s => s.Products).ToList();
                var lisProduct = new List<ProductViewModel>();
                foreach (var item in list)
                {
                    lisProduct.AddRange(item);
                }

                var productWasAdd = lisProduct.Where(w => listShop.Select(s => s.Id).Contains(w.Id)).Select(s => new ProductOrderViewModel
                {
                    ProductId = s.Id.Value,
                    Quantity = listShop.Where(w => w.Id == s.Id).Count()
                }).ToList();

                #endregion
                model.UserAddressId = address.Id;
                model.Products = productWasAdd;
                model.Date = DateTime.Now;

                var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                var resultService = await apiService.CreateOrder(model);
                CleanCardSession();
            }
            catch (Exception)
            {
                TempDataMessages(new string[] { "Ocurrio un error al intentar finalizar el pedido." }, TagHelperStatusEnum.Error.ToString());
            }
            
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> AddCard(Guid? id, Guid? categoryId)
        {
            AddCardSession(new ProductViewModel { Id = id });
            return RedirectToAction("Index", new { id = categoryId });
        }

        public async Task<IActionResult> DeleteCard(Guid? id, Guid? categoryId)
        {
            DeleteCardSession(new ProductViewModel { Id = id });
            return RedirectToAction("Index", new { id = categoryId });
        }

        public async Task<IActionResult> ChangeStatus(Guid orderId, int status)
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);

            var resultService = await apiService.ChangeOrderStatus(orderId, status);

            ModelStateMessage<ApiResponse<BasicResponse>>(resultService, true);

            return RedirectToAction("Index");
        }
    }
}