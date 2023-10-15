using SmartPhoneStore.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;

namespace SmartPhoneStore.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public int CategoryId { set; get; }
        public int Stock { set; get; }
        public decimal OriginalPrice { set; get; }
        public DateTime DateCreated { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string ThumbnailImage { get; set; }
        public string ProductImage { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }
}