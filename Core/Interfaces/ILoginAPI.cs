using Newtonsoft.Json;
using Refit;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.User;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tacovela.MVC.Core.Interfaces
{

    public interface ILoginAPI
    {
        [Post("/user/login")]
        Task<ApiResponse<LoginResponse>> LoginUser([Body] UserLoginViewModel user);
        
        [Post("/user/post")]
        Task<ApiResponse<LoginResponse>> RegisterUser(UserRegisterViewModel userRegister);

        [Get("/user/sendmailforgotpassword")]
        Task<ApiResponse<LoginResponse>> ResetPasswordVerify(string email);

        [Post("/user/resetpassword")]
        Task<ApiResponse<LoginResponse>> ResetPassword([Body] ResetPasswordViewModel user);

        [Get("/user/sendmailvalidation")]
        Task<ApiResponse<LoginResponse>> SendMailValidation(string email);

        [Get("/user/confirmemail")]
        Task<ApiResponse<LoginResponse>> ConfirmEmail(string email);
        
    }
}
