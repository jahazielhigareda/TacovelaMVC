using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Authentication;
using Tacovela.MVC.Models.User;

namespace Tacovela.MVC.Core.Interfaces
{
    public interface IAuthenticationAPI
    {
        [Post("/user/login")]
        Task<ApiResponse<BasicResponse<LoginUserViewModel>>> Login([Body] UserLoginViewModel user);

        [Post("/user")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> Register(UserRegisterViewModel userRegister);

        [Get("/user/sendmailforgotpassword")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> ResetPasswordVerify(string email);

        [Post("/user/resetpassword")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> ResetPassword([Body] ResetPasswordViewModel user);

        [Get("/user/sendmailvalidation")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> SendMailValidation(string email);

        [Get("/user/confirmemail")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> ConfirmEmail(string email);
    }
}
