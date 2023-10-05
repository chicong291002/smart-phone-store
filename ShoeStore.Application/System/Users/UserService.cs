using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartPhoneStore.Data.Entities;
using SmartPhoneStore.ViewModels.Common;
using SmartPhoneStore.ViewModels.System.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        //UserManager là một lớp được sử dụng để quản lý thông tin người dùng như tạo, xóa, cập nhật thông tin người dùng,
        // và thực hiện các hoạt động liên quan đến quản lý người dùng
        //SignInManager là một lớp được sử dụng để xác thực người dùng trong quá trình đăng nhập và đăng xuất
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager
            , IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            // Tìm xem tên user có tồn tại hay không
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            // Trả về một SignInResult, tham số cuối là IsPersistent kiểu bool
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            //lockOutOnfaiture : khi lockout nhieu qua thi khoa account
            
/*            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>(new string("Mật khẩu không đúng"));
            }*/

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return new ApiErrorResult<UserViewModel>("User không tồn tại");

            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewModel()
            {
                Name = user.Name,
                email = user.Email,
                Address = user.Address,
                phoneNumber = user.PhoneNumber,
                userName = user.UserName,
                Id = user.Id,
            };

            foreach (var role in roles)
            {
                userVm.Roles = role.ToString();
            }

            return new ApiSuccessResult<UserViewModel>(userVm);
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(request.keyword))
            {
                query = query.Where(x =>
                   x.UserName.Contains(request.keyword)
                || x.Email.Contains(request.keyword)
                || x.PhoneNumber.Contains(request.keyword)
                || x.Name.Contains(request.keyword));
            }

            // 3 Paging

            int totalRow = await query.CountAsync();
            var data = await query.Skip(request.PageIndex - 1).Take(request.PageSize).
                Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    email = x.Email,
                    phoneNumber = x.PhoneNumber,
                    userName = x.UserName,

                }).ToListAsync();
            //4 Select and projection
            var pageResult = new PagedResult<UserViewModel>()
            {
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserViewModel>>(pageResult);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.userName);

            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại ");
            }

            if (await _userManager.FindByEmailAsync(request.email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }

            user = new AppUser()
            {
                UserName = request.userName,
                Name = request.Name,
                Address = request.Address,
                Email = request.email,
                PhoneNumber = request.phoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.passWord);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Address = request.Address;
            user.Email = request.Email;
            user.Name = request.Name;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var result = await _userManager.FindByIdAsync(id.ToString());
            if (result == null)
            {
                return new ApiErrorResult<bool>("Not found User");
            }
            var user = await _userManager.DeleteAsync(result);
            if (user.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Delete not Successful");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại ");
            }

            var removeRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name);
            await _userManager.RemoveFromRolesAsync(user, removeRoles);

            var addRoles = request.Roles.Where(x => x.Selected == true).Select(x => x.Name).ToList();
            foreach (var role in addRoles)
            {
                if (await _userManager.IsInRoleAsync(user, role) == false)
                {
                    await _userManager.AddToRoleAsync(user, role); 
                }
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<UserViewModel>> GetByUserName(string userName)
        {
            var user = await _userManager.FindByIdAsync(userName);
            if (user == null) return new ApiErrorResult<UserViewModel>("User không tồn tại");

            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewModel()
            {
                Name = user.Name,
                Address = user.Address,
                email = user.Email,
                phoneNumber = user.PhoneNumber,
                userName = user.UserName,
                Id = user.Id,
            };
            if (roles.Count == 0)
            {
                userVm.Roles = "customer";
            }
            else
            {
                foreach (var role in roles)
                {
                    userVm.Roles = role.ToString();
                }
            }

            return new ApiSuccessResult<UserViewModel>(userVm);
        }

        public async Task<ApiResult<bool>> ConfirmEmail(ConfirmEmailViewModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user == null)
                return new ApiErrorResult<bool>($"Không tìm thấy người dùng có email {request.email}");
            var result = await _userManager.ConfirmEmailAsync(user, request.token);
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return new ApiSuccessResult<bool>();
            }

            return new ApiErrorResult<bool>("Đổi mật khẩu không thành công");
        }

        public async Task<ApiResult<string>> ForgotPassword(ForgotPasswordViewModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return new ApiSuccessResult<string>(token);
            }
            return new ApiErrorResult<string>($"Không thể khôi phục mật khẩu");
        }

        public async Task<ApiResult<bool>> ResetPassword(ResetPasswordViewModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new ApiErrorResult<bool>($"Không tìm thấy người dùng có email {request.Email}");
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            return new ApiSuccessResult<bool>();
        }
    }
}
