using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public DateTime DateCreated { set; get; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<Carts> Carts { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<ProductImage> ProductImages { get; set; }

    }
}
