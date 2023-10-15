using SmartPhoneStore.ViewModels.Common;
using SmartPhoneStore.ViewModels.System.Users;

namespace SmartPhoneStore.AdminApp.ApiIntegration.Users
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);

        Task<ApiResult<string>> Register(RegisterRequest request);

        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<PagedResult<UserViewModel>>> GetAllUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<UserViewModel>> GetByUserName(string userName);

        Task<ApiResult<bool>> ChangePassword(ChangePasswordViewModel model);

        Task<ApiResult<bool>> ConfirmEmail(ConfirmEmailViewModel model);

        Task<ApiResult<string>> ForgotPassword(ForgotPasswordViewModel model);

        Task<ApiResult<bool>> ResetPassword(ResetPasswordViewModel model);

        Task<List<UserViewModel>> GetAll();
    }
}
