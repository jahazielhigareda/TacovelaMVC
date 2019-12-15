using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Authentication;
using Tacovela.MVC.Models.Category;
using Tacovela.MVC.Models.Common;
using Tacovela.MVC.Models.Ingredient;
using Tacovela.MVC.Models.Product;
using Tacovela.MVC.Models.User;

namespace Tacovela.MVC.Core.Interfaces
{
    public interface IUserAPI
    {
        #region User
        
        [Post("/user/getbyid"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> GetById(Guid id);

        [Post("/user/edit"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse<UserViewModel>>> Edit(UserViewModel model);

        [Post("/user/resetpassword"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> ResetPassword([Body] ChangePasswordViewModel user);

        [Post("/user/get"), Headers("Authorization: Bearer")]
        Task<ApiResponse<ListResultViewModel<List<UserViewModel>>>> GetList(UserViewModel filter);

        [Post("/user/get"), Headers("Authorization: Bearer")]
        Task<ApiResponse<ListResultViewModel<List<UserViewModel>>>> GetList(FilterViewModel<UserViewModel> filter);

        [Post("/user/createadmin"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> CreateAdmin(CreateAdminViewModel model);

        [Delete("/user/deleteadmin"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> DeleteAdmin(Guid id);

        [Post("/user/resetpasswordadmin"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> ResetPasswordAdmin(ResetPasswordViewModel user);

        #endregion

        #region Address User        

        [Get("/user/getaddressbyiduser"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse<UserAddressViewModel>>> GetAddressByIdUser(Guid id);

        [Post("/user/address"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> CreateAddress(UserAddressViewModel model);

        [Put("/user/address"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> UpdateAddress(UserAddressViewModel model);

        #endregion

        #region Product

        [Get("/product"), Headers("Authorization: Bearer")]
        Task<ListResultViewModel<List<CategoryViewModel>>> ProductList();

        #endregion
    }
}
