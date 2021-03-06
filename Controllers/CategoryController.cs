﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tacovela.MVC.Models.Api;

namespace Tacovela.MVC.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IOptions<Api> enforcerApi) : base(enforcerApi) { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}