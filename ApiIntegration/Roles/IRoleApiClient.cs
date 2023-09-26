using ShoeStore.ViewModels.Common;
using ShoeStore.ViewModels.System.Roles;

namespace ShoeStore.AdminApp.ApiIntegration.Roles
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAllRoles();
    }
}
