using SmartPhoneStore.ViewModels.Common;
using SmartPhoneStore.ViewModels.System.Roles;

namespace SmartPhoneStore.AdminApp.ApiIntegration.Roles
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAllRoles();
    }
}
