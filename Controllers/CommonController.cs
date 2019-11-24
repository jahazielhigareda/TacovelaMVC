using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tacovela.MVC.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using System.Net;
using Newtonsoft.Json;
using Tacovela.MVC.Core.Enums;

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

        public void TempDataMessage<T>(ApiResponse<BasicResponse> resultService, bool clean = false)
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

        public void ModelStateMessage<T>(ApiResponse<BasicResponse> resultService, bool clean = false)
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

        public void TempDataMessage<T>(ApiResponse<BasicResponse<T>> resultService, bool clean = false)
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

        public void ModelStateMessage<T>(ApiResponse<BasicResponse<T>> resultService, bool clean = false)
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

        public T GetData<T>(ApiResponse<BasicResponse<T>> resultService)
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

        public T GetPagginationData<T>(ApiResponse<ListResultViewModel<T>> resultService)
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

    }
}
