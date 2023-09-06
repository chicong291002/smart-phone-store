using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoeStore.Application.Catalog.Products.DTOS;
using ShoeStore.Application.DTOS;
using ShoeStore.Application.System.Users.DTOS;
using ShoeStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.System.Users
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

        public async Task<string> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            //lockOutOnfaiture : khi lockout nhieu qua thi khoa account

            if (!result.Succeeded)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(request.keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.keyword)
                || x.PhoneNumber.Contains(request.keyword));
            }

            // 3 Paging

            int totalRow = await query.CountAsync();
            var data = await query.Skip(request.pageIndex - 1).Take(request.pageSize).
                Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    email = x.Email,
                    phoneNumber = x.PhoneNumber,
                    userName = x.UserName,
                }).ToListAsync();
            //4 Select and projection
            var pageResult = new PagedResult<UserViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.userName);

            if (user != null)
            {
                return false;
            }

            if (await _userManager.FindByEmailAsync(request.email) != null)
            {
                return false;
            }

            user = new AppUser()
            {
                UserName = request.userName,
                FirstName = request.firstName,
                LastName = request.lastName,
                Dob = request.Dob,
                Email = request.email,
                PhoneNumber = request.phoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.passWord);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }


    }
}
