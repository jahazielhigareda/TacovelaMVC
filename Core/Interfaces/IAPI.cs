using Newtonsoft.Json;
using Refit;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.User;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Ingredient;
using Tacovela.MVC.Models.Common;
using Tacovela.MVC.Models.Product;

namespace Tacovela.MVC.Core.Interfaces
{
    public interface IAPI
    {
        #region User

        [Post("/user/login")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> LoginUser([Body] UserLoginViewModel user);

        [Post("/user")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> RegisterUser(UserRegisterViewModel userRegister);

        [Get("/user/sendmailforgotpassword")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> ResetPasswordVerify(string email);

        [Post("/user/resetpassword")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> ResetPassword([Body] ResetPasswordViewModel user);

        [Get("/user/sendmailvalidation")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> SendMailValidation(string email);

        [Get("/user/confirmemail")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> ConfirmEmail(string email);

        [Post("/user/edit")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> EditUser(UserViewModel model);

        #endregion

        #region Address User

        [Post("/user/getbyid")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> GetUserById(Guid id);

        [Get("/user/getaddressbyiduser")]
        Task<ApiResponse<BasicResponse<UserAddressViewModel>>> GetAddressByIdUser(Guid id);

        [Post("/user/address")]
        Task<ApiResponse<BasicResponse>> CreateAddress(UserAddressViewModel model);

        [Put("/user/address")]
        Task<ApiResponse<BasicResponse>> UpdateAddress(UserAddressViewModel model);



        #endregion

        #region Ingredient

        [Post("/ingredient/Get")]
        Task<ListResultViewModel<List<IngredientViewModel>>> IngredientList(IngredientViewModel filter);

        #endregion

        #region Product

        [Get("/product")]
        Task<ListResultViewModel<List<ProductViewModel>>> ProductList();

        #endregion
    }
}
