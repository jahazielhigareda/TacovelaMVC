using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using Tacovela.MVC.Core.Enums;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.User;

namespace Tacovela.MVC.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IOptions<Api> enforcerApi) : base(enforcerApi)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var apiService = RestService.For<ILoginAPI>(_enforcerApi.Url);

                    //var resultService = await apiService.LoginUser(model);
                    //if (resultService.IsSuccessStatusCode)
                    //{
                    //    var result = resultService.Content;
                    //    var userSession = result.Data;
                    //    //userSession.ImageProfile = string.IsNullOrEmpty(userSession.ImageProfile) ? "//placehold.it/60" : userSession.ImageProfile;
                    //    //HttpContext.Session.SetString("Token", result.Data.Token);
                    //    //HttpContext.Session.SetObjectAsJson("UserSession", userSession);
                    //    return RedirectToAction("Index", "Home");
                    //}
                    //else
                    //{
                    //    var error = JsonConvert.DeserializeObject<BasicResponse<UserResponse>>(resultService.Error.Content);
                    //    if (Convert.ToInt32(error.ErrorCode) == (int)ErrorApiRequestEnums.CuentaNoActivada)
                    //    {
                    //        //var result = apiService.SendMailValidation(model.Email);
                    //        return RedirectToAction("SendMailActivationAccount", "Authentication", new { email = model.Email });
                    //    }
                    //    else
                    //    {
                    //        HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                    //    }
                    //}

                }
                catch (Exception ex)
                {
                    ExeptionError(ex);
                    return View(model);
                }
            }

            ModelState.Clear();
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}