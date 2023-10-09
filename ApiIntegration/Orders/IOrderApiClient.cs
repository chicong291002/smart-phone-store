using SmartPhoneStore.ViewModels.Catalog.Orders;
using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Common;
using SmartPhoneStore.ViewModels.Sales;

namespace ShoeStore.AdminApp.ApiIntegration.Products
{
    public interface IOrderApiClient
    {
        Task<string> CreateOrder(CheckoutRequest request);

        Task<PagedResult<OrderViewModel>> GetPagings(GetOrderPagingRequest request);

        Task<bool> UpdateOrderStatus(int id);

        Task<bool> CancelOrderStatus(int id);

        Task<OrderByUserViewModel> GetOrderByUser(string id);

        Task<OrderViewModel> GetOrderById(int orderId);
    }
}
