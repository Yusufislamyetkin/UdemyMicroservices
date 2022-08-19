﻿using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Input;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SıgnIn(SigninInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();

    }
}