using Microsoft.AspNetCore.Http;
using ShoeStore.ViewModels.Catalog.Categories;
using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Application.Catalog.Products.DTOS
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Thông số kỹ thuật")]
        public string Description { set; get; }
        [Display(Name = "Ảnh đại diện")]
        public IFormFile Image { get; set; }

        [Display(Name = "Ảnh đầy đủ")]
        public IFormFile ProductImage { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
