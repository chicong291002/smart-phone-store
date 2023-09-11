using ShoeStore.Application.Catalog.Categories.DTOS;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.DTOS;
using ShoeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Categories
{
    public interface ICategoryService
    {   
        Task<List<CategoryViewModel>> GetAllCategorys();
    }
}
