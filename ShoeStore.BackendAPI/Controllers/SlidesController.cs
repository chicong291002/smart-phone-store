using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.Utilities.Slides;
using System.Threading.Tasks;

namespace ShoeStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SlidesController : ControllerBase
    {
        private readonly ISlideService _slideService;
        public SlidesController(ISlideService slideService)
        {
            _slideService = slideService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _slideService.GetAll();
            return Ok(roles);
        }
    }
}
