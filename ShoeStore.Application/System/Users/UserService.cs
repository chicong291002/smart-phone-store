using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

            if (result.Succeeded)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role,string.Join(";",roles))
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

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new AppUser()
            {
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
