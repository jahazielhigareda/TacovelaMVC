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

        public IActionResult EditProfile()
        {
            var userId = GetUserSession().Id;

            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var model = apiService.GetUserById(userId).Result.Content.Data;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = GetUserSession().Id;

                var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                var resultService = await apiService.EditUser(model);
                if (resultService.IsSuccessStatusCode)
                {
                    HandleMessages(new string[] { "Editción Completa." }, TagHelperStatusEnums.Success.ToString());
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                    HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                }
            }
            return View();
        }

        public IActionResult EditAddress()
        {
            var userId = GetUserSession().Id;

            var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
            var model = apiService.GetAddressByIdUser(userId).Result.Content.Data;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(UserAddressViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.UserId = GetUserSession().Id;

                var apiService = RestServiceExtension<IAPI>.For(_enforcerApi.Url, GetUserSession().Token);
                var address = apiService.GetAddressByIdUser(model.UserId).Result.Content.Data;
                if (address == null)
                {
                    var resultService = await apiService.CreateAddress(model);
                    if (resultService.IsSuccessStatusCode)
                    {
                        HandleMessages(new string[] { "Editción Completa." }, TagHelperStatusEnums.Success.ToString());
                    }
                    else
                    {
                        var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                        HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                    }
                }
                else
                {
                    model.Id = address.Id;
                    var resultService = await apiService.UpdateAddress(model);
                    if (resultService.IsSuccessStatusCode)
                    {
                        HandleMessages(new string[] { "Editción Completa." }, TagHelperStatusEnums.Success.ToString());
                    }
                    else
                    {
                        var error = JsonConvert.DeserializeObject<BasicResponse<UserViewModel>>(resultService.Error.Content);
                        HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
                    }
                }
                
            }
            return View();
        }


        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}