using SmartPhoneStore.Data.Enums;
using static Azure.Core.HttpHeader;

namespace SmartPhoneStore.Data.Entities
{
    public class Order
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }
        public DateTime OrderDate { set; get; }
        public int CouponId { get; set; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipPhoneNumber { set; get; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public OrderStatus Status { set; get; } 
        public List<OrderDetail> OrderDetails { get; set; }
        public AppUser AppUser { get; set; }

        public Coupon Coupon { get; set; }
    }
}