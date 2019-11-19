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
        Task<ApiResponse<BasicResponse<UserResponse>>> LoginUser([Body] UserLoginViewModel user);
        
        [Post("/user/post")]
        Task<ApiResponse<BasicResponse<UserResponse>>> RegisterUser(UserRegisterViewModel userRegister);

        [Get("/user/sendmailforgotpassword")]
        Task<ApiResponse<BasicResponse<UserResponse>>> ResetPasswordVerify(string email);

        [Post("/user/resetpassword")]
        Task<ApiResponse<BasicResponse<UserResponse>>> ResetPassword([Body] ResetPasswordViewModel user);

        [Get("/user/sendmailvalidation")]
        Task<ApiResponse<BasicResponse<UserResponse>>> SendMailValidation(string email);

        [Get("/user/confirmemail")]
        Task<ApiResponse<BasicResponse<UserResponse>>> ConfirmEmail(string email);
        
    }
}
