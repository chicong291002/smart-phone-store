using SmartPhoneStore.ViewModels.Catalog.Coupons;
using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Common;

namespace ShoeStore.AdminApp.ApiIntegration.Products
{
    public interface ICouponApiClient
    {
        Task<List<CouponViewModel>> GetAll();

        Task<CouponViewModel> GetById(int id);

        Task<bool> CreateCoupon(CouponCreateRequest request);

        Task<bool> UpdateCoupon(CouponUpdateRequest request);

        Task<bool> DeleteCoupon(int id);

        Task<PagedResult<CouponViewModel>> GetAllPaging(GetProductPagingRequest request);
    }
}
