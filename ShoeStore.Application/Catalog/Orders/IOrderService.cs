using SmartPhoneStore.ViewModels.Catalog.Orders;
using SmartPhoneStore.ViewModels.Common;
using SmartPhoneStore.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Catalog.Orders
{
    public interface IOrderService
    {
        int Create(CheckoutRequest request);

        Task<PagedResult<OrderViewModel>> GetAllPaging(GetOrderPagingRequest request);

        Task<ApiResult<bool>> UpdateOrderStatus(int orderId);

        Task<ApiResult<bool>> CancelOrderStatus(int orderId);

        Task<OrderByUserViewModel> GetOrderByUser(string userId);

        List<OrderDetailViewModel> GetOrderDetails(int orderId);

        OrderViewModel GetOrderById(int orderId);
    }
}
