using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Authentication;
using Tacovela.MVC.Models.Category;
using Tacovela.MVC.Models.Ingredient;
using Tacovela.MVC.Models.Order;
using Tacovela.MVC.Models.Product;
using Tacovela.MVC.Models.User;

namespace Tacovela.MVC.Core.Interfaces
{
    public interface IAPI
    {
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

        [Get("/product/getbyidcategorybyanonymous")]
        Task<ApiResponse<ListResultViewModel<List<ProductViewModel>>>> GetProdutByIdCategoryByAnonymous(Guid? id);

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

        [Get("/category/getallbyanonymous")]
        Task<ApiResponse<ListResultViewModel<List<CategoryViewModel>>>> GetCategoryListByAnonymous();
        #endregion


        [Post("/order"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> CreateOrder([FromBody] OrderViewModel model);
    }
}
