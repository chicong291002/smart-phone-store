using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Application.System.Roles;
using ShoeStore.Data.Entities;

namespace ShoeStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService) {
            _roleService = roleService;
        }
        public async Task<IActionResult> GetAllRoles()
        {
            var roles =await _roleService.GetAllRoles();
            return Ok(roles);
        }
    }
}
