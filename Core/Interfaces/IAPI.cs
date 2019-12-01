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

        [Delete("/product"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> DeleteProduct(Guid id);

        #endregion

        #region Category

        [Get("/category"), Headers("Authorization: Bearer")]
        Task<ListResultViewModel<List<CategoryViewModel>>> GetCategory();

        #endregion
    }
}
