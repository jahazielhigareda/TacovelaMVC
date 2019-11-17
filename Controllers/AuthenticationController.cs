using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using System;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Enums;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.User;

namespace Tacovela.MVC.Controllers
{
    public class AuthenticationController : CommonController
    {
        public AuthenticationController(IOptions<Api> enforcerApi) : base(enforcerApi)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiService = RestService.For<ILoginAPI>(_enforcerApi.Url);
                //try
                //{
                var resultService = await apiService.LoginUser(model);
                if (resultService.IsSuccessStatusCode)
                {
                    var result = resultService.Content;
                    HttpContext.Session.SetString("Token", result.Data.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<LoginResponse>(resultService.Error.Content);
                    if (Convert.ToInt32(error.ErrorCode) == (int)ErrorApiRequestEnums.CuentaNoActivada)
                    {
                        //var result = apiService.SendMailValidation(model.Email);
                        return RedirectToAction("SendMailActivationAccount", "Authentication", new { email = model.Email });
                    }
                    else
                    {
                        HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                    }
                }
                //}
                //catch (Exception)
                //{

                //}

            }
            return PartialView("Index", model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiService = RestService.For<ILoginAPI>(_enforcerApi.Url);
                var resultService = await apiService.RegisterUser(model);
                if (resultService.IsSuccessStatusCode)
                {
                    var result = resultService.Content;
                    //HttpContext.Session.SetString("Token", result.Data.Token);
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<LoginResponse>(resultService.Error.Content);
                    HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                }
                //var serviceApi = RestService.For<ILoginAPI>(_enforcerApi.Url);
                //var result = serviceApi.RegisterUser(model).Result;
                //if (result.Success)
                //{
                //    //HttpContext.Session.SetString("Token", result.Data.Token);
                //    return RedirectToAction("Login", "Home");
                //}
            }
            return PartialView("Register", model);
        }

        [Route("/reset-password")]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("/reset-password")]
        public async Task<IActionResult> ResetPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiService = RestService.For<ILoginAPI>(_enforcerApi.Url);
                var resultService = await apiService.ResetPasswordVerify(model.Email);
                if (resultService.IsSuccessStatusCode)
                {
                    var result = resultService.Content;
                    return RedirectToAction("SendMailForgotPassword", "Authentication");
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<LoginResponse>(resultService.Error.Content);
                    HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                }
            }
            return PartialView("ResetPassword", model);
        }

        [Route("/send-mail-reset-password")]
        public IActionResult SendMailForgotPassword()
        {
            return View();
        }

        [Route("/reset-password-success")]
        public IActionResult ResetPasswordSuccess()
        {
            return View();
        }

        public IActionResult ResetPasswordVerification(Guid key)
        {
            if (key == Guid.Empty)
            {
                return RedirectToAction("Index", "Authentication");
            }
            var model = new ResetPasswordViewModel { Id = key };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordVerification(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiService = RestService.For<ILoginAPI>(_enforcerApi.Url);
                var resultService = await apiService.ResetPassword(model);
                if (resultService.IsSuccessStatusCode)
                {
                    var result = resultService.Content;
                    return RedirectToAction("ResetPasswordSuccess", "Authentication");
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<LoginResponse>(resultService.Error.Content);
                    HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                }
            }
            return PartialView("ResetPasswordVerification", model);
        }

        public async Task<IActionResult> ActivateAccount(string email)
        {
            if (ModelState.IsValid)
            {
                var apiService = RestService.For<ILoginAPI>(_enforcerApi.Url);
                var resultService = await apiService.ConfirmEmail(email);
                if (resultService.IsSuccessStatusCode)
                {
                    //var result = resultService.Content;
                    //HandleMessages(new string[] { "Se activo la cuenta." }, TagHelperStatusEnums.Success.ToString());                 
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<LoginResponse>(resultService.Error.Content);
                    HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                }
            }
            return View();
        }

        public IActionResult SendMailActivationAccount(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Authentication");
            }
            var model = new ActivationAccountViewModel { Email = email };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> SendMailActivationAccount(ActivationAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiService = RestService.For<ILoginAPI>(_enforcerApi.Url);
                var resultService = await apiService.SendMailValidation(model.Email);
                if (resultService.IsSuccessStatusCode)
                {
                    //var result = resultService.Content;
                    //HandleMessages(new string[] { "Se envio el correo de activación a su bandeja de correo" }, TagHelperStatusEnums.Success.ToString());
                    return RedirectToAction("SendMailActivationAccountSuccess", "Authentication");
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<LoginResponse>(resultService.Error.Content);
                    HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                }
            }
            return PartialView("SendMailActivationAccount", model);
        }

        public IActionResult SendMailActivationAccountSuccess()
        {
            return View();
        }
    }
}