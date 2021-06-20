using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Services
{
    public interface IAuthorizeService
    {
        (AccessToken accessToken, RefreshToken refreshToken) CreateAccessToken(Guid applicationId, IEnumerable<string> scopes);
        (AccessToken accessToken, RefreshToken refreshToken) CreateAccessToken(AuthorizationCode authorizationCode, IEnumerable<string> scopes);
        AccessToken CreateAccessToken(RefreshToken refreshToken);
        AuthorizationCode CreateAuthorizationCode(Guid applicationId, Guid accountId);
        AccessToken GetAccessToken(string token);
        RefreshToken GetRefreshToken(string token);
        Application GetApplication(string clientId);
        AuthorizationCode GetAuthorizationCode(string code);
    }
}
