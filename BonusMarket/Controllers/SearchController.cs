using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BonusMarket.Models;
using Models;
using BusinessLayer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using UnidecodeSharpFork;
using Models.EntityModels.ViewModels;
using WebShop;
using System.ServiceModel;
using BonusMarket.Factories;
using System.Net.Http;
using BonusMarket.Shared.Models.Core;
using BonusMarket.Shared.Models.Core.Paging;
using BonusMarket.Shared.Repository.Brand;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BonusMarket.Controllers
{
    public class SearchController : BaseController
    {
        private readonly BonusMarket.Shared.Repository.ProductRepository _productRepository;
        private readonly ILogger<SearchController> _logger;
        private readonly BrandRepository _brandRepository;
        
        public SearchController(BonusMarket.Shared.Repository.ProductRepository productRepository, ILogger<SearchController> logger,
            BrandRepository brandRepository
            )
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _logger = logger;
        }

        [Route("/searchv2page")]
        public IActionResult SearchV2Page([FromQuery] string hyName, [FromQuery] string engName,
            [FromQuery] string ruName, [FromQuery] string OrderBy,
            [FromQuery] string hyBrand, [FromQuery] string engBrand, [FromQuery] string ruBrand, int CurrentPage = 1)
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var currentCultureCode = requestCulture.RequestCulture.UICulture.TwoLetterISOLanguageName;
            this.ViewData["BaseModel"] = this.BaseModel;
            bool isBrandSearch = false;
            bool isProductSearch = false;
            if (!String.IsNullOrEmpty(hyName) || !String.IsNullOrEmpty(engName) || !String.IsNullOrEmpty(ruName))
                isProductSearch = true;
            if (!String.IsNullOrEmpty(hyBrand) || !String.IsNullOrEmpty(engBrand) || !String.IsNullOrEmpty(ruBrand))
                isBrandSearch = true;

            int? foundBrandId = null;
            PagedResult<Brand> brands = new PagedResult<Brand>();
            if (isBrandSearch)
            {
                brands = _brandRepository.Search(new BrandFilter()
                {
                    HyName = hyName,
                    EngName = engName,
                    RuName = ruName,
                    HyBrand = hyBrand,
                    EngBrand = engBrand,
                    RuBrand = ruBrand,
                    OrderBy = OrderBy,
                    LanguageCode = currentCultureCode,
                    PageSize = 10000,
                    
                });
                if (brands.Results.Count > 0)
                {
                    foundBrandId = brands.Results[0].Id;
                }
            }

            var filters = new ProductFilter(CurrentPage, 12)
            {
                HyName = hyName,
                EngName = engName,
                RuName = ruName,
                HyBrand = hyBrand,
                EngBrand = engBrand,
                RuBrand = ruBrand,
                OrderBy = OrderBy,
                LanguageCode = currentCultureCode
            };
            if (foundBrandId != null)
                filters.BrandId = foundBrandId;

            var vm = new SearchV2VM(_productRepository.Search(filters), hyName, engName, ruName, hyBrand, engBrand,
                ruBrand)
            {
                OrderBy = OrderBy,
            };

            vm.brands = brands;
            
            return View(vm);
        }
        
        
        [Route("/searchv2")]
        public IActionResult SearchV2([FromQuery] string hyName, [FromQuery] string engName, [FromQuery] string ruName, [FromQuery] string OrderBy,
            [FromQuery] string hyBrand, [FromQuery] string engBrand, [FromQuery] string ruBrand )
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var currentCultureCode = requestCulture.RequestCulture.UICulture.TwoLetterISOLanguageName;
            this.ViewData["BaseModel"] = this.BaseModel;
            bool isBrandSearch = false;
            bool isProductSearch = false;
            if (!String.IsNullOrEmpty(hyName) || !String.IsNullOrEmpty(engName) || !String.IsNullOrEmpty(ruName))
                isProductSearch = true;
            if (!String.IsNullOrEmpty(hyBrand) || !String.IsNullOrEmpty(engBrand) || !String.IsNullOrEmpty(ruBrand))
                isBrandSearch = true;

            int? foundBrandId = null;
            PagedResult<Brand> brands = new PagedResult<Brand>();
            if (isBrandSearch)
            {
                brands = _brandRepository.Search(new BrandFilter()
                {
                    HyName = hyName,
                    EngName = engName,
                    RuName = ruName,
                    HyBrand = hyBrand,
                    EngBrand = engBrand,
                    RuBrand = ruBrand,
                    OrderBy = OrderBy,
                    LanguageCode = currentCultureCode,
                    PageSize = 10000,
                    
                });
                if (brands.Results.Count > 0)
                {
                    foundBrandId = brands.Results[0].Id;
                }
            }

            var filters = new ProductFilter(1, 1000)
            {
                HyName = hyName,
                EngName = engName,
                RuName = ruName,
                HyBrand = hyBrand,
                EngBrand = engBrand,
                RuBrand = ruBrand,
                OrderBy = OrderBy,
                LanguageCode = currentCultureCode
            };
            if (foundBrandId != null)
                filters.BrandId = foundBrandId;

            var vm = new SearchV2VM(_productRepository.Search(filters), hyName, engName, ruName, hyBrand, engBrand,
                ruBrand)
            {
                OrderBy = OrderBy,
            };

            vm.brands = brands;
            return Ok(JsonConvert.SerializeObject(vm));
        }
    }
}