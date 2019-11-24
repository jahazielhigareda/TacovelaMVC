using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using System.Threading.Tasks;
using Tacovela.MVC.Core.Enums;
using Tacovela.MVC.Core.Extensions;
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
    }
}