namespace ShoeStore.Data.Entities
{
    public class Carts
    {
        public int Id { get; set; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public Product Product { get; set; }

        public AppUser AppUser { get; set; }
    }
}