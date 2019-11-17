using Microsoft.AspNetCore.Http;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Models.Api
{
    public class HttpHelper<T>
    {
        public T Request(string url, HttpContext context)
        {
            return RestService.For<T>(url,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () =>
                            Task.FromResult(context.Session.GetString("JWToken"))
                    });
        }
    }
}
