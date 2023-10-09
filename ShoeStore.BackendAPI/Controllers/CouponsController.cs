using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPhoneStore.Application.Catalog.Coupons;
using SmartPhoneStore.ViewModels.Catalog.Products;
using System.Threading.Tasks;

/*namespace SmartPhoneStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponsController(
            ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coupons = await _couponService.GetAll();
            return Ok(coupons);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetProductPagingRequest request)
        {
            var coupons = await _couponService.GetAllPaging(request);
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coupon = await _couponService.GetById(id);
            return Ok(coupon);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CouponCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var couponId = await _couponService.Create(request);

            if (couponId == 0)
                return BadRequest();

            var coupon = await _couponService.GetById(couponId);

            return CreatedAtAction(nameof(GetById), new { id = couponId }, coupon);
        }

        // HttpPut: update toàn phần
        [HttpPut("updateCoupon")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] CouponUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _couponService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedResult = await _couponService.Delete(id);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}*/
