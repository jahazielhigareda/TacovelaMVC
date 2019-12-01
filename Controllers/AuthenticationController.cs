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
            //return RedirectToAction("Index","Order");
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
                        //HttpContext.Session.SetObjectAsJson("UserSession", data);
                        SetUserSession(data);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception e)
                {
                    ModelStateMessages(new string[] { "Error de conexión." }, TagHelperStatusEnum.Error.ToString());
                }
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
                ModelStateMessage<BasicResponse>(resultService);
                if (resultService.IsSuccessStatusCode)
                {
                    //var result = resultService.Content;
                    return RedirectToAction("Index", "Authentication");
                }
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
                ModelStateMessage<BasicResponse>(resultService);
                if (resultService.IsSuccessStatusCode)
                {
                    //var result = resultService.Content;
                    return RedirectToAction("SendMailForgotPassword", "Authentication");
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
                var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                var resultService = await apiService.ResetPassword(model);
                ModelStateMessage<BasicResponse>(resultService);
                if (resultService.IsSuccessStatusCode)
                {
                    var result = resultService.Content;
                    return RedirectToAction("ResetPasswordSuccess", "Authentication");
                }
            }
            return PartialView("ResetPasswordVerification", model);
        }

        public async Task<IActionResult> ActivateAccount(string email)
        {
            if (ModelState.IsValid)
            {
                var apiService = RestService.For<IAuthenticationAPI>(_enforcerApi.Url);
                var resultService = await apiService.ConfirmEmail(email);
                ModelStateMessage<BasicResponse>(resultService);
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
                ModelStateMessage<BasicResponse>(resultService);
                if (resultService.IsSuccessStatusCode)
                {
                    return RedirectToAction("SendMailActivationAccountSuccess", "Authentication");
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