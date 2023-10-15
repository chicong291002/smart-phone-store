using Microsoft.AspNetCore.Http;
using SmartPhoneStore.ViewModels.Catalog.Categories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPhoneStore.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Display(Name = "Danh mục")]
        public int CategoryId { set; get; }
        [Display(Name = "Số lượng")]
        public int Stock { set; get; }
        [Display(Name = "Thông số kỹ thuật")]
        public string Description { set; get; }
        [Display(Name = "Giá tiền")]
        public decimal Price { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public IFormFile ThumbnailImage { get; set; }

        [Display(Name = "Ảnh đầy đủ")]
        public IFormFile ProductImage { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
