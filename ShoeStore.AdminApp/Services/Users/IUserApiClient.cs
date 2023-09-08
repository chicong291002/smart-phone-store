using Microsoft.AspNetCore.Http;
using ShoeStore.Application.Common;
using ShoeStore.Application.DTOS;
using ShoeStore.Application.System.Users.DTOS;

namespace ShoeStore.AdminApp.Services.Users
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<PagedResult<UserViewModel>>> GetAllUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);
    }
}
