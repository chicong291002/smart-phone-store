namespace SmartPhoneStore.Data.Entities
{
    public class OrderDetail
    {

        public int orderId { get; set; }
        public int productId { get; set; }
        public int Quantity { set; get; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}