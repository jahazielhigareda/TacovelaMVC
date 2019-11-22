using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tacovela.MVC.Core.Extensions
{
    public class RestServiceExtension<T>
    {
        public static T For(string url, string token)
        {
            return RestService.For<T>(url,
                    new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () => Task.FromResult(token)
                    });
        }
    }
}
