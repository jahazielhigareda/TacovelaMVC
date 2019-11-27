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
        [Post("/auth/login")]
        Task<ApiResponse<BasicResponse<LoginUserViewModel>>> Login([Body] UserLoginViewModel user);

        [Post("/auth/register")]
        Task<ApiResponse<BasicResponse>> Register(UserRegisterViewModel userRegister);

        [Get("/auth/sendmailforgotpassword")]
        Task<ApiResponse<BasicResponse>> ResetPasswordVerify(string email);

        [Post("/auth/resetpassword")]
        Task<ApiResponse<BasicResponse>> ResetPassword([Body] ResetPasswordViewModel user);

        [Get("/auth/sendmailvalidation")]
        Task<ApiResponse<BasicResponse>> SendMailValidation(string email);

        [Get("/auth/confirmemail")]
        Task<ApiResponse<BasicResponse>> ConfirmEmail(string email);
    }
}
