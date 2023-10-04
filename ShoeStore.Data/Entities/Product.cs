namespace SmartPhoneStore.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { set; get; }
        public string Thumbnail { get; set; }
        public string ProductImage { get; set; }
        public decimal OriginalPrice { set; get; }
        public DateTime DateCreated { set; get; }
        public List<Carts> Carts { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
