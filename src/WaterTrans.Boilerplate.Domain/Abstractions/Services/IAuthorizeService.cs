using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Services
{
    public interface IAuthorizeService
    {
        AccessToken CreateAccessToken(Guid applicationId, IEnumerable<string> scopes);
        AccessToken CreateAccessToken(AuthorizationCode authorizationCode, IEnumerable<string> scopes);
        AuthorizationCode CreateAuthorizationCode(Guid applicationId, Guid accountId);
        AccessToken GetAccessToken(string token);
        Application GetApplication(string clientId);
        AuthorizationCode GetAuthorizationCode(string code);
    }
}
