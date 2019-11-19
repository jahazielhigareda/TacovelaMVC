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

namespace Tacovela.MVC.Controllers
{
    public class CommonController : Controller
    {
        protected readonly Api _enforcerApi;
        public CommonController(IOptions<Api> enforcerApi)
        {
            _enforcerApi = enforcerApi.Value;
        }

        protected void HandleMessages(string[] errors, string messageType)
        {
            foreach (var erro in errors)
            {
                ModelState.AddModelError(messageType, erro);
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
    }
}
