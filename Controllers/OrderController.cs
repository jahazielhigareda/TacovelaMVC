using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
using Tacovela.MVC.Models.Ingredient;
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
            return RedirectToAction("Category");
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
                if (address == null)
                {
                    TempDataMessages(new string[]
                    {
                        "No ha agregado una dirección en su cuenta.",
                        "Para hacerlo tendra que ir a Su perfil y en la sección de Dirección especificar su dirección."
                    }, TagHelperStatusEnum.Error.ToString());
                    return RedirectToAction("Index");
                }
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

                TempDataMessages(new string[] { "Su pedido fue enviado y registrado con succeso." }, TagHelperStatusEnum.Success.ToString());
                return RedirectToAction("List", "Order");

            }
            catch (Exception)
            {
                TempDataMessages(new string[] { "Ocurrio un error al intentar finalizar el pedido." }, TagHelperStatusEnum.Error.ToString());
                return RedirectToAction("Index");
            }

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

        public IActionResult Category()
        {
            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var categories = apiService.GetCategory().Result.Data.Where(w => w.IsActive).ToList();

            return View(categories);
        }

        public async Task<IActionResult> Product(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return RedirectToAction("Category", "Order");
            }

            var apiService = RestService.For<IAPI>(_enforcerApi.Url);
            var resultService = await apiService.GetProdutByIdCategoryByAnonymous(id);

            var list = GetData<List<ProductViewModel>>(resultService);
            return View(list);
        }

        public IActionResult ProductDetail(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return RedirectToAction("Category", "Order");
            }

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

            //return View();
        }

        public async Task<IActionResult> Test(Guid id, int prodictQuantity, string ingredienstArray)
        {
            if (id == null || id == Guid.Empty || prodictQuantity <= 0)
            {
                return RedirectToAction("Category", "Order");
            }

            
            var listIngredient = JsonConvert.DeserializeObject<List<AddProductIngredientViewModel>>(ingredienstArray);
            var listIngredientAdd = new List<ProductOrderIngredientViewModel>();
            foreach (var item in listIngredient)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    listIngredientAdd.Add(new ProductOrderIngredientViewModel
                    {
                        IngredientId = item.IngredientId,
                        Add = true
                    });
                }
            }

            var model = new OrderViewModel();

            #region Get Id Address

            var userId = GetUserSession().Id;

            var apiServiceAddress = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var resultAddress = await apiServiceAddress.GetAddressByIdUser(userId);
            var address = GetData<UserAddressViewModel>(resultAddress);
            if (address == null)
            {
                TempDataMessages(new string[]
                {
                        "No ha agregado una dirección en su cuenta.",
                        "Para hacerlo tendra que ir a Su perfil y en la sección de Dirección especificar su dirección."
                }, TagHelperStatusEnum.Error.ToString());
                return RedirectToAction("Index");
            }
            #endregion

            model.UserAddressId = address.Id;
            model.Products = new List<ProductOrderViewModel> {
                new ProductOrderViewModel{
                    ProductId = id,
                    Ingredients = listIngredientAdd,
                    Quantity = prodictQuantity
                }
            };
            model.Date = DateTime.Now;
            model.Status = OrderStatus.InCard;

            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var user = GetUserSession();

            var order = apiService.GetOrder(user.Id, OrderStatus.InCard).Result.Data.FirstOrDefault();
            if (order == null)
            {
                var resultService = await apiService.CreateOrder(model);
            }
            else
            {
                model.Id = order.Id;
                var resultService = await apiService.UpdateOrder(model);
            }
            //var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            //CleanCardSession();

            TempDataMessages(new string[] { "Se agrego al carrito con succeso." }, TagHelperStatusEnum.Success.ToString());
            return RedirectToAction("Index", "Order");

        }

        public IActionResult FinishOrder()
        {
            return View();
        }
    }
}