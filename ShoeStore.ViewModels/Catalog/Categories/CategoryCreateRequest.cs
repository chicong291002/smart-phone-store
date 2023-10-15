using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.ViewModels.Catalog.Categories
{
    public class CategoryCreateRequest
    {
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
    }
}
