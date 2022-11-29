using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models.Search;
using TECH.Service;

namespace TECH.Controllers.Components

{
    [ViewComponent(Name = "ProductSearchComponent")]
    public class ProductSearchComponent : ViewComponent
    {
        private readonly IProductsService _productsService;
        public ProductSearchComponent(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ProductViewModelSearch ProductModelViewSearch)
        {           
            var products = _productsService.GetAllPaging(ProductModelViewSearch);
            if (products.Results != null && products.Results.Count > 0)
            {
                var data = products.Results.ToList();
                return View(data);
            }
            return View(null);
        }
    }
}