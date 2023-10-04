using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Utilities.Slides;
using System.Collections.Generic;

namespace SmartPhoneStore.WebApp.Models
{
    public class HomeViewModel
    {
        public List<SlideViewModel> Slides { get; set; }

        public List<ProductViewModel> FeaturedProducts { get; set; }

        public List<ProductViewModel> LatestProducts { get; set; }
    }
}
