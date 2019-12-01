using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Authentication;
using Tacovela.MVC.Models.Category;
using Tacovela.MVC.Models.Ingredient;
using Tacovela.MVC.Models.Product;
using Tacovela.MVC.Models.User;

namespace Tacovela.MVC.Core.Interfaces
{
    public interface IAPI
    {
        #region User

        [Post("/user/login")]
        Task<ApiResponse<BasicResponse<LoginUserViewModel>>> LoginUser([Body] UserLoginViewModel user);

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

        [Post("/user/getbyid"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> GetUserById(Guid id);

        [Post("/user/edit"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> EditUser(UserViewModel model);

        #endregion

        #region Address User        

        [Get("/user/getaddressbyiduser"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse<UserAddressViewModel>>> GetAddressByIdUser(Guid id);

        [Post("/user/address"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> CreateAddress(UserAddressViewModel model);

        [Put("/user/address"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> UpdateAddress(UserAddressViewModel model);



        #endregion

        #region Ingredient

        [Get("/ingredient"), Headers("Authorization: Bearer")]
        Task<ListResultViewModel<List<ProductIngredientViewModel>>> GetIngredients(Guid? productId = null);

        [Post("/ingredient/Get"), Headers("Authorization: Bearer")]
        Task<ListResultViewModel<List<IngredientViewModel>>> IngredientList(IngredientViewModel filter);

        #endregion

        #region Product

        [Get("/product"), Headers("Authorization: Bearer")]
        Task<ListResultViewModel<List<ProductViewModel>>> ProductList(Guid? id = null);

        [Multipart]
        [Put("/product"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> UpdateProduct([Query] ProductViewModel model, [AliasAs("stream")]StreamPart stream);

        [Multipart]
        [Post("/product"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> CreateProduct([Query] ProductViewModel model, [AliasAs("stream")]StreamPart stream);

        [Delete("/product"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> DeleteProduct(Guid id);

        #endregion

        #region Category

        [Get("/category"), Headers("Authorization: Bearer")]
        Task<ListResultViewModel<List<CategoryViewModel>>> GetCategory(Guid? id = null);

        [Multipart]
        [Post("/category"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> CreateCategory([Query] CategoryViewModel model, [AliasAs("stream")]StreamPart stream);

        [Multipart]
        [Put("/category"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> UpdateCategory([Query] CategoryViewModel model, [AliasAs("stream")]StreamPart stream);

        #endregion

    }
}
