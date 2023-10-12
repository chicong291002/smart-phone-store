using Microsoft.EntityFrameworkCore;
using SmartPhoneStore.Data.EF;
using SmartPhoneStore.Data.Entities;
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
    public class CouponService : ICouponService
    {
        private readonly SmartPhoneStoreDbContext _context;

        public CouponService(SmartPhoneStoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CouponCreateRequest request)
        {
            var coupon = new Coupon()
            {
                Code = request.Code,
                Count = request.Count,
                Promotion = request.Promotion,
                Describe = request.Describe
            };

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            return coupon.Id;
        }

        public async Task<int> Update(CouponUpdateRequest request)
        {
            var coupon = await _context.Coupons.FindAsync(request.Id);
            if (coupon == null) throw new Exception($"Không thể tìm coupon có ID: {request.Id} ");

            coupon.Code = request.Code;
            coupon.Count = request.Count;
            coupon.Promotion = request.Promotion;
            coupon.Describe = request.Describe;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int couponId)
        {
            var coupon = await _context.Coupons.FindAsync(couponId);
            if (coupon == null) throw new Exception($"Không thể tìm coupon có ID: {coupon} ");

            _context.Coupons.Remove(coupon);

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<CouponViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            var query = from c in _context.Coupons
                        select new { c };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.c.Code.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CouponViewModel()
                {
                    Id = x.c.Id,
                    Code = x.c.Code,
                    Count = x.c.Count,
                    Promotion = x.c.Promotion,
                    Describe = x.c.Describe
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CouponViewModel>()
            {
                TotalRecord = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<List<CouponViewModel>> GetAll()
        {
            var query = from c in _context.Coupons
                        select new { c };

            return await query.Select(x => new CouponViewModel()
            {
                Id = x.c.Id,
                Code = x.c.Code,
                Count = x.c.Count,
                Promotion = x.c.Promotion,
                Describe = x.c.Describe
            }).ToListAsync();
        }

        public async Task<CouponViewModel> GetById(int id)
        {
            var query = from c in _context.Coupons
                        where c.Id == id
                        select new { c };

            return await query.Select(x => new CouponViewModel()
            {
                Id = x.c.Id,
                Code = x.c.Code,
                Count = x.c.Count,
                Promotion = x.c.Promotion,
                Describe = x.c.Describe
            }).FirstOrDefaultAsync();
        }
    }
}
