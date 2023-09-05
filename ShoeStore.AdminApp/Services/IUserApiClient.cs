﻿using ShoeStore.Application.System.Users.DTOS;

namespace ShoeStore.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);

        Task<bool> Rigister(RegisterRequest request);
    }
}