using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Categories.DTOS
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ParentId { get; set; }

    }
}
