using ShoeStore.Application.Common;
using ShoeStore.Application.System.Roles;

namespace ShoeStore.AdminApp.Services.Roles
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAllRoles();
    }
}
