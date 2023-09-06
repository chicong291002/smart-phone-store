using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoeStore.Application.Common;
using ShoeStore.Application.DTOS;
using ShoeStore.Application.System.Users.DTOS;

namespace ShoeStore.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);
    }
}
