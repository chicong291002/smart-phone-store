using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoeStore.Application.System.Users;
using ShoeStore.Application.System.Users.DTOS;

namespace ShoeStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous] //chua login van goi duoc 
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(resultToken.ResultObj))
            {
                return BadRequest(resultToken);
            }

            return Ok(resultToken); //tra ve 1 chuoi token 
        }

        [HttpPost("register")]
        [AllowAnonymous]    
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request);
            if (!result.ResultObj)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //Put : https://localhost:7204/api/Users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {
            //Guid la kieu unit identity 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);    
        }

        //https://localhost:7204/api/paging?pageIndex=1&pageSize=10&keyword=""
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllUsersPaging([FromQuery] GetUserPagingRequest request)
        {
            var products = await _userService.GetUsersPaging(request);
            return Ok(products); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var users = await _userService.GetById(id);
            return Ok(users);
        }
    }
}
