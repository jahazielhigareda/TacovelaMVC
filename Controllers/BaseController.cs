using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using System;
using Tacovela.MVC.Core.Enums;
using Tacovela.MVC.Core.Extensions;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Authentication;

namespace Tacovela.MVC.Controllers
{
    public class BaseController : CommonController
    {
        public BaseController(IOptions<Api> enforcerApi) : base(enforcerApi)
        {
        }

        public void BasicResponse<T>(T model, ApiResponse<BasicResponse> resultService, bool clean = false)
        {
            if (resultService.IsSuccessStatusCode)
            {
                HandleMessages(new string[] { "Operación Completada." }, TagHelperStatusEnums.Success.ToString());
                if (clean)
                {
                    ModelState.Clear();
                    ViewData["success"] = "Operación Completada.";
                }                
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
            }
        }

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
        public T AwaitDataResult<T>(T model, ApiResponse<BasicResponse<T>> resultService)
        {
            if (resultService.IsSuccessStatusCode)
            {
                model = resultService.Content.Data;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
            }
            return model;
        }

        public T AwaitPaginationResult<T>(T model, ApiResponse<ListResultViewModel<T>> resultService)
        {
            if (resultService.IsSuccessStatusCode)
            {
                model = resultService.Content.Data;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<BasicResponse<T>>(resultService.Error.Content);
                HandleMessages(error.Errors, TagHelperStatusEnums.Error.ToString());
            }
            return (T)Activator.CreateInstance(typeof(T), model);
        }


        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    var controller = ControllerContext.RouteData.Values["controller"].ToString();
        //    var action = ControllerContext.RouteData.Values["action"].ToString();

        //    //var menuSystem = HttpContext.Session.GetObjectFromJson<List<MenuSystemViewModel>>("MenuSystem");
        //    var userSession = HttpContext.Session.GetObjectFromJson<UserViewModel>("UserSession");
        //    ///var token = Microsoft.AspNetCore.Http.SessionExtensions.Get(HttpContext.Session, "Token");
        //    //HttpContext.Session.TryGetValue("Token");
        //    //token = new byte[12];
        //    if (userSession == null)
        //    {
        //        if (("/" + controller + "/" + action) != "/Authentication/Index")
        //        {
        //            context.Result = new RedirectResult("/Authentication/Index");
        //            //RedirectToAction("Index","Home");
        //        }
        //    }
        //    else
        //    {
        //        //if (("/" + controller + "/" + action) == "/Home/Index")
        //        //{
        //        //    context.Result = new RedirectResult("/Home/Dashboard");
        //        //}
        //        //else
        //        //{
        //        //if (menuSystem == null)
        //        //{
        //        //    GetMenuSystem();
        //        //}
        //        //else
        //        //{
        //        //    var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        //        //    if (!isAjax)
        //        //    {
        //        //        //need optimitation
        //        //        foreach (var menu in menuSystem)
        //        //        {
        //        //            if (menu.SubMenu != null)
        //        //            {
        //        //                foreach (var subMenu in menu?.SubMenu)
        //        //                {
        //        //                    if (subMenu.IsActive)
        //        //                    {
        //        //                        subMenu.IsActive = false;
        //        //                    }
        //        //                    if (subMenu.Url.Equals("/" + controller + "/" + action))
        //        //                    {
        //        //                        subMenu.IsActive = true;
        //        //                    }
        //        //                }
        //        //            }

        //        //            var controllerMenu = menu.Url.Split('/');
        //        //            if (controllerMenu[1].Equals(controller))
        //        //            {
        //        //                HttpContext.Session.SetObjectAsJson("MenuSidebar", menu);
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //        //}
        //    }
        //    base.OnActionExecuted(context);
        //}


        public LoginUserViewModel GetUserSession()
        {
            return HttpContext.Session.GetObjectFromJson<LoginUserViewModel>("UserSession");
        }
    }
}
