using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartPhoneStore.ViewModels.Catalog.Categories;
using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Common;

namespace SmartPhoneStore.WebApp.Models
{
    public class ProductCategoryViewModel
    {
        public CategoryViewModel Category { get; set; }

        public PagedResult<ProductViewModel> Products { get; set; }
    }
}
