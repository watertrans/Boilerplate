using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Domain.Abstractions.Services
{
    public interface IAuthorizeService
    {
        AccessToken CreateAccessToken(Guid applicationId, IList<string> scopes);
        AccessToken CreateAccessToken(Guid applicationId, Guid accountId, IList<string> scopes);
        AuthorizationCode CreateAuthorizationCode(Guid applicationId, Guid accountId);
        AccessToken GetAccessToken(string token);
        Application GetApplication(string clientId);
        AuthorizationCode GetAuthorizationCode(string code);
        void UseAuthorizationCode(string code);
    }
}
