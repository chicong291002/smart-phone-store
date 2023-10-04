using SmartPhoneStore.ViewModels.Catalog.Categories;
using SmartPhoneStore.ViewModels.Catalog.Products;
using System.Collections.Generic;

namespace SmartPhoneStore.WebApp.Models
{
    public class ProductDetailViewModel
    {
        public CategoryViewModel Category { get; set; }

        public ProductViewModel Product { get; set; }

        public List<ProductViewModel> RelatedProducts  { get; set; }
    }
}
