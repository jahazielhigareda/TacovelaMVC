using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Core.Interfaces;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Authentication;
using Tacovela.MVC.Models.Common;
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

        public async Task<IActionResult> EditProfile()
        {
            var userId = GetUserSession().Id;

            var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var result = await apiService.GetById(userId);
            var model = GetData<UserViewModel>(result);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = GetUserSession().Id;

                var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                var resultService = await apiService.Edit(model);
                ModelStateMessage<UserViewModel>(resultService);

                //var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                //var resultService = await apiService.Edit(model);
                //if (resultService.IsSuccessStatusCode)
                //{
                //    TempDataMessages(new string[] { "Editción Completa." }, TagHelperStatusEnum.Success.ToString());
                //}
                //else
                //{
                //    var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //    TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
                //}
            }
            return View();
        }

        public async Task<IActionResult> EditAddress()
        {
            var userId = GetUserSession().Id;

            var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var result = await apiService.GetAddressByIdUser(userId);
            var model = GetData<UserAddressViewModel>(result);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(UserAddressViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.UserId = GetUserSession().Id;

                var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                var result = await apiService.GetAddressByIdUser(model.UserId);
                var address = GetData<UserAddressViewModel>(result);

                if (address == null)
                {
                    var subResult = await apiService.CreateAddress(model);
                    ModelStateMessage<BasicResponse>(subResult);
                }
                else
                {
                    model.Id = address.Id;
                    var subResult = await apiService.UpdateAddress(model);
                    ModelStateMessage<BasicResponse>(subResult);
                }


                //var address = apiService.GetAddressByIdUser(model.UserId).Result.Content.Data;
                //if (address == null)
                //{
                //    var resultService = await apiService.CreateAddress(model);
                //    if (resultService.IsSuccessStatusCode)
                //    {
                //        TempDataMessages(new string[] { "Editción Completa." }, TagHelperStatusEnum.Success.ToString());
                //    }
                //    else
                //    {
                //        var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //        TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
                //    }
                //}
                //else
                //{
                //    model.Id = address.Id;
                //    var resultService = await apiService.UpdateAddress(model);
                //    if (resultService.IsSuccessStatusCode)
                //    {
                //        TempDataMessages(new string[] { "Editción Completa." }, TagHelperStatusEnum.Success.ToString());
                //    }
                //    else
                //    {
                //        var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                //        TempDataMessages(error.Errors, TagHelperStatusEnum.Error.ToString());
                //    }
                //}

            }
            return View();
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = GetUserSession().Id;

                var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                var resultService = await apiService.ResetPassword(model);
                TempDataMessage<UserViewModel>(resultService, true);
            }
            return View();
        }

        public async Task<IActionResult> ListAdmin()
        {
            var model = new List<UserViewModel>();

            var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var resultService = await apiService.GetList(new FilterViewModel<UserViewModel> { ApplyFilter = true, Filter = new UserViewModel { Type = 2 } });
            model = GetPagginationData<List<UserViewModel>>(resultService);

            return View(model);
        }


        public async Task<IActionResult> NewEditAdmin(Guid? id)
        {
            var model = new UserViewModel();
            ViewData["SubTitle"] = "Nuevo";
            ViewData["SubTitleDescription"] = "Para crear un nuevo administrador debera llenar el siguiente formulario.";

            if (id != null && id != Guid.Empty)
            {
                var apiService = RestServiceExtension<IUserAPI>.For(_enforcerApi.Url, GetUserSession().Token);

                var result = await apiService.GetById(id.Value);
                model = GetData<UserViewModel>(result);

                ViewData["SubTitle"] = "Editar";
                ViewData["SubTitleDescription"] = "Para editar el administrador debera llenar el siguiente formulario.";
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewEditAdmin(UserRegisterViewModel model)
        {
            return View();
        }

    }
}