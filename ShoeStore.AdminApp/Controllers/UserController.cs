using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ShoeStore.AdminApp.Services;
using ShoeStore.Application.System.Users.DTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoeStore.AdminApp.Controllers
{
    //dang nhap moi dc vao`
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 2)
        {
            //default parameters
            /*            var session = HttpContext.Session.GetString("Token");*/
            var request = new GetUserPagingRequest()
            {
                keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetAllUsersPaging(request);
            return View(data.ResultObj); // ra duoc pageUser
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.Register(request);
            if (result.IsSuccessed)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);  //lỗi model bussiness
            //message tu api truyen vao 
            return View(request); // ko co thi tra ve view voi du~ lieu co san
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var userUpdateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    email = user.email,
                    firstName = user.firstName,
                    lastName = user.lastName,
                    phoneNumber = user.phoneNumber,
                    Id = id
                };
                return View(userUpdateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(); //lỗi validation modelstate trả về 
            }

            var result = await _userApiClient.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request); // ko co thi tra ve view voi du~ lieu co san
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.Delete(id);
            if (result.IsSuccessed)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);  //lỗi model bussiness
            //message tu api truyen vao 
            return View(result); // ko co thi tra ve view voi du~ lieu co san
        }
    }
}
