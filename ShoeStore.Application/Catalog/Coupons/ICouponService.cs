using SmartPhoneStore.ViewModels.Catalog.Coupons;
using SmartPhoneStore.ViewModels.Catalog.Products;
using SmartPhoneStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.Application.Catalog.Coupons
{
    public interface ICouponService
    {
        Task<int> Create(CouponCreateRequest request);

        Task<int> Update(CouponUpdateRequest request);

        Task<int> Delete(int couponId);

        Task<PagedResult<CouponViewModel>> GetAllPaging(GetProductPagingRequest request);

        Task<List<CouponViewModel>> GetAll();

        Task<CouponViewModel> GetById(int id);
    }
}
