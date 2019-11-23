using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacovela.MVC.Models.Api;
using Tacovela.MVC.Models.Ingredient;

namespace Tacovela.MVC.Core.Interfaces
{
    public interface IIngredientAPI
    {
        [Post("/ingredient/get"), Headers("Authorization: Bearer")]
        Task<ApiResponse<ListResultViewModel<List<IngredientViewModel>>>> GetList(IngredientViewModel filter);

        [Get("/ingredient/id"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse<IngredientViewModel>>> GetById(Guid id);

        [Post("/ingredient"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> Create([FromBody] IngredientViewModel model);

        [Put("/ingredient"), Headers("Authorization: Bearer")]
        Task<ApiResponse<BasicResponse>> Edit([FromBody] IngredientViewModel model);
    }
}
