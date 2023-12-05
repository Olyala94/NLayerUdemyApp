using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesController : CustumBaseController
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // api/categories/GetSingleCategorByIdWithProducts/2  <- şu id'yi alabilmek için [action/{categoryId}] yazıyoruz...[action] - bizim otomotik olarak method ismiizi alır...
        [HttpGet("[action]/{categoryId}")]  //id ismini ne verdiysek orda da aynı ismi olması lazım'ki Framework Mapliyebilmesi için.
        public async Task<IActionResult> GetSingleCategorByIdWithProductsAsync(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategorByIdWithProductsAsync(categoryId));
        }
    }
}
