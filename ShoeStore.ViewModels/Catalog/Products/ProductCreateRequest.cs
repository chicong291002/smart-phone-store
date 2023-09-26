﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public int Stock { set; get; }
        public string Description { get; set; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public IFormFile Image { get; set; }
    }
}