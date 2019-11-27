using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
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



        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = ControllerContext.RouteData.Values["controller"].ToString();
            var action = ControllerContext.RouteData.Values["action"].ToString();

            //var menuSystem = HttpContext.Session.GetObjectFromJson<List<MenuSystemViewModel>>("MenuSystem");
            var userSession = GetUserSession();
            ///var token = Microsoft.AspNetCore.Http.SessionExtensions.Get(HttpContext.Session, "Token");
            //HttpContext.Session.TryGetValue("Token");
            //token = new byte[12];
            if (userSession == null)
            {
                if (("/" + controller + "/" + action) != "/Authentication/Index")
                {
                    context.Result = new RedirectResult("/Authentication/Index");
                    //RedirectToAction("Index","Home");
                }
            }
            else
            {
                if (controller == "Authentication")
                {
                    context.Result = new RedirectResult("/Home/Index");
                }
                //else
                //{
                //if (menuSystem == null)
                //{
                //    GetMenuSystem();
                //}
                //else
                //{
                //    var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                //    if (!isAjax)
                //    {
                //        //need optimitation
                //        foreach (var menu in menuSystem)
                //        {
                //            if (menu.SubMenu != null)
                //            {
                //                foreach (var subMenu in menu?.SubMenu)
                //                {
                //                    if (subMenu.IsActive)
                //                    {
                //                        subMenu.IsActive = false;
                //                    }
                //                    if (subMenu.Url.Equals("/" + controller + "/" + action))
                //                    {
                //                        subMenu.IsActive = true;
                //                    }
                //                }
                //            }

                //            var controllerMenu = menu.Url.Split('/');
                //            if (controllerMenu[1].Equals(controller))
                //            {
                //                HttpContext.Session.SetObjectAsJson("MenuSidebar", menu);
                //            }
                //        }
                //    }
                //}
                //}
            }
            base.OnActionExecuted(context);
        }


        
    }
}
