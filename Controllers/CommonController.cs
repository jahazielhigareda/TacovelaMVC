using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Tacovela.MVC.Core.Enums;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Authentication;
using Tacovela.MVC.Models.Product;

namespace Tacovela.MVC.Controllers
{
    public class CommonController : Controller
    {
        protected readonly Api _enforcerApi;

        public CommonController(IOptions<Api> enforcerApi)
        {
            _enforcerApi = enforcerApi.Value;
        }

        protected void TempDataMessages(string[] errors, string messageType)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var error in errors)
            {
                dictionary.Add(messageType, error);
            }
            TempData[GlobalApplicationEnum.TempDataMessage.ToString()] = dictionary;
        }

        protected void ModelStateMessages(string[] errors, string messageType)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(messageType, error);
            }
        }

        protected void ExeptionError(Exception ex)
        {
            if (ex.InnerException is ApiException)
            {
                if (((ApiException)ex.InnerException).StatusCode == HttpStatusCode.Unauthorized)
                {
                    ModelState.AddModelError("", "Acesso não autorizado.");
                }
                else if (((ApiException)ex.InnerException).StatusCode == HttpStatusCode.InternalServerError)
                {
                    ModelState.AddModelError("", "Ocorreu um erro na solitação. Tente novamente.");
                }
                else
                {
                    var resultError = JsonConvert.DeserializeObject<ResultViewModelBase>(((ApiException)ex.InnerException).Content);

                    if (resultError != null && resultError.Errors != null)
                    {
                        foreach (var erro in resultError.Errors)
                        {
                            ModelState.AddModelError("", erro);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", ex.InnerException.Message);
                    }
                }
            }
        }

        #region Handler Message

        protected void TempDataMessage<T>(ApiResponse<BasicResponse> resultService, bool clean = false)
        {
            if (resultService.IsSuccessStatusCode)
            {
                TempDataMessages(new string[] { "Operación Completada." }, TagHelperStatusEnum.Success.ToString());
                if (clean)
                {
                    ModelState.Clear();
                }
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
            }
        }

        protected void ModelStateMessage<T>(ApiResponse<BasicResponse> resultService, bool clean = false)
        {
            if (resultService.IsSuccessStatusCode)
            {
                ModelStateMessages(new string[] { "Operación Completada." }, TagHelperStatusEnum.Success.ToString());
                if (clean)
                {
                    ModelState.Clear();
                }
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                ModelStateMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
            }
        }

        protected void TempDataMessage<T>(ApiResponse<BasicResponse<T>> resultService, bool clean = false)
        {
            if (resultService.IsSuccessStatusCode)
            {
                TempDataMessages(new string[] { "Operación Completada." }, TagHelperStatusEnum.Success.ToString());
                if (clean)
                {
                    ModelState.Clear();
                }
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
            }
        }

        protected void ModelStateMessage<T>(ApiResponse<BasicResponse<T>> resultService, bool clean = false)
        {
            if (resultService.IsSuccessStatusCode)
            {
                ModelStateMessages(new string[] { "Operación Completada." }, TagHelperStatusEnum.Success.ToString());
                if (clean)
                {
                    ModelState.Clear();
                }
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                ModelStateMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
            }
        }

        #endregion

        //public T AwaitDataResult<T>(T model, ApiResponse<BasicResponse<T>> resultService)
        //{
        //    var list = new List<T>();
        //    if (resultService.IsSuccessStatusCode)
        //    {
        //        model = resultService.Content.Data;
        //        list.Add(model);
        //    }
        //    else
        //    {
        //        var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
        //        HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
        //    }
        //    var ss = (T)Activator.CreateInstance(typeof(List<T>), list);
        //    return (T)Activator.CreateInstance(typeof(T), model);
        //}

        #region Get Data API

        protected T GetData<T>(ApiResponse<BasicResponse<T>> resultService)
        {
            if (resultService.IsSuccessStatusCode)
            {
                return resultService.Content.Data;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
            }
            return default(T);
        }
        protected T GetData<T>(ApiResponse<ListResultViewModel<T>> resultService)
        {
            if (resultService.IsSuccessStatusCode)
            {
                return resultService.Content.Data;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
            }
            return default(T);
        }

        protected T GetPagginationData<T>(ApiResponse<ListResultViewModel<T>> resultService)
        {
            if (resultService.IsSuccessStatusCode)
            {
                return resultService.Content.Data;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
            }
            return default(T);
        }


        #endregion

        public LoginUserViewModel GetUserSession()
        {
            return HttpContext.Session.GetObjectFromJson<LoginUserViewModel>("UserSession");
        }
        public void SetUserSession(LoginUserViewModel data)
        {
            HttpContext.Session.SetObjectAsJson("UserSession", data);
        }

        public void AddCardSession(ProductViewModel data)
        {
            var cardshop = GetCardSession();
            if (cardshop == null)
            {
                var listShop = new List<ProductViewModel> { data };
                HttpContext.Session.SetObjectAsJson("CardShopSession", listShop);
            }
            else
            {
                cardshop.Add(data);
                HttpContext.Session.SetObjectAsJson("CardShopSession", cardshop);
            }
        }

        public void DeleteCardSession(ProductViewModel data)
        {
            var cardshop = GetCardSession();
            var listDelete = cardshop.Where(w => w.Id == data.Id).ToList();
            foreach (var item in listDelete)
            {
                var delete = cardshop.FirstOrDefault(f => f.Id == item.Id);
                cardshop.Remove(delete);
            }
            HttpContext.Session.SetObjectAsJson("CardShopSession", cardshop);
        }

        public List<ProductViewModel> GetCardSession()
        {
            return HttpContext.Session.GetObjectFromJson<List<ProductViewModel>>("CardShopSession");
        }

        public void CleanCardSession()
        {
            HttpContext.Session.SetObjectAsJson("CardShopSession", null);
        }


    }
}
