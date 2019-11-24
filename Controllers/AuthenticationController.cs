using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using System;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Enums;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Authentication;
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
                try
                {
                    var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                    var resultService = await apiService.Login(model);
                    ModelStateMessage<LoginUserViewModel>(resultService);
                    if (resultService.IsSuccessStatusCode)
                    {
                        var data = GetData<LoginUserViewModel>(resultService);

                        data.ImageProfile = string.IsNullOrEmpty(data.ImageProfile) ? "//placehold.it/60" : data.ImageProfile;
                        HttpContext.Session.SetObjectAsJson("UserSession", data);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception e)
                {

                }
                


                //var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                //try
                //{
                //var resultService = await apiService.LoginUser(model);
                //if (resultService.IsSuccessStatusCode)
                //{
                //    var result = resultService.Content;
                //    var userSession = result.Data;
                //    userSession.ImageProfile = string.IsNullOrEmpty(userSession.ImageProfile) ? "//placehold.it/60" : userSession.ImageProfile;
                //    HttpContext.Session.SetObjectAsJson("UserSession", userSession);
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //    if (Convert.ToInt32(error.ErrorCode) == (int)GlobalApplicationEnum.CuentaNoActivada)
                //    {
                //        //var result = apiService.SendMailValidation(model.Email);
                //        return RedirectToAction("SendMailActivationAccount", "Authentication", new { email = model.Email });
                //    }
                //    else
                //    {
                //        TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
                //    }
                //}
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
                var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                var resultService = await apiService.Register(model);
                ModelStateMessage<UserViewModel>(resultService);
                if (resultService.IsSuccessStatusCode)
                {
                    //var result = resultService.Content;
                    return RedirectToAction("Index", "Authentication");
                }

                //var apiService = RestService.For<IAPI>(_enforcerApi.Url);
                //var resultService = await apiService.RegisterUser(model);
                //if (resultService.IsSuccessStatusCode)
                //{
                //    var result = resultService.Content;
                //    //return RedirectToAction("Dashboard", "Home");
                //    return RedirectToAction("Index", "Authentication");
                //}
                //else
                //{
                //    var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //    TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
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
                var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                var resultService = await apiService.ResetPasswordVerify(model.Email);
                ModelStateMessage<UserViewModel>(resultService);
                if (resultService.IsSuccessStatusCode)
                {
                    //var result = resultService.Content;
                    return RedirectToAction("SendMailForgotPassword", "Authentication");
                }


                //var apiService = RestService.For<IAPI>(_enforcerApi.Url);
                //var resultService = await apiService.ResetPasswordVerify(model.Email);
                //if (resultService.IsSuccessStatusCode)
                //{
                //    var result = resultService.Content;
                //    return RedirectToAction("SendMailForgotPassword", "Authentication");
                //}
                //else
                //{
                //    var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //    TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
                //}
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
                var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                var resultService = await apiService.ResetPassword(model);
                ModelStateMessage<UserViewModel>(resultService);
                if (resultService.IsSuccessStatusCode)
                {
                    var result = resultService.Content;
                    return RedirectToAction("ResetPasswordSuccess", "Authentication");
                }

                //var apiService = RestService.For<IAPI>(_enforcerApi.Url);
                //var resultService = await apiService.ResetPassword(model);
                //if (resultService.IsSuccessStatusCode)
                //{
                //    var result = resultService.Content;
                //    return RedirectToAction("ResetPasswordSuccess", "Authentication");
                //}
                //else
                //{
                //    var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //    TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
                //}
            }
            return PartialView("ResetPasswordVerification", model);
        }

        public async Task<IActionResult> ActivateAccount(string email)
        {
            if (ModelState.IsValid)
            {
                var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                var resultService = await apiService.ConfirmEmail(email);
                ModelStateMessage<UserViewModel>(resultService);

                //var apiService = RestService.For<IAPI>(_enforcerApi.Url);
                //var resultService = await apiService.ConfirmEmail(email);
                //if (resultService.IsSuccessStatusCode)
                //{
                //    //var result = resultService.Content;
                //    //HandleMessages(new string[] { "Se activo la cuenta." }, TagHelperStatusEnums.Success.ToString());                 
                //}
                //else
                //{
                //    var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //    TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
                //}
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
                var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                var resultService = await apiService.SendMailValidation(model.Email);
                ModelStateMessage<UserViewModel>(resultService);
                if (resultService.IsSuccessStatusCode)
                {
                    return RedirectToAction("SendMailActivationAccountSuccess", "Authentication");
                }

                //var apiService = RestService.For<IAPI>(_enforcerApi.Url);
                //var resultService = await apiService.SendMailValidation(model.Email);
                //if (resultService.IsSuccessStatusCode)
                //{
                //    //var result = resultService.Content;
                //    //HandleMessages(new string[] { "Se envio el correo de activación a su bandeja de correo" }, TagHelperStatusEnums.Success.ToString());
                //    return RedirectToAction("SendMailActivationAccountSuccess", "Authentication");
                //}
                //else
                //{
                //    var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //    TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
                //}
            }
            return PartialView("SendMailActivationAccount", model);
        }

        public IActionResult SendMailActivationAccountSuccess()
        {
            return View();
        }
    }
}