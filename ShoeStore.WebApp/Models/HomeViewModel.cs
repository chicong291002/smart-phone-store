using ShoeStore.ViewModels.Catalog.Products;
using ShoeStore.ViewModels.Utilities.Slides;
using System.Collections.Generic;

namespace ShoeStore.WebApp.Models
{
    public class HomeViewModel
    {
        public List<SlideViewModel> Slides { get; set; }

        public List<ProductViewModel> FeaturedProducts { get; set; }

        public List<ProductViewModel> LatestProducts { get; set; }
    }
}
