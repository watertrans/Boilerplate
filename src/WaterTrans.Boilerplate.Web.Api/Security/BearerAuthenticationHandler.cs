using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Abstractions.OS;
using WaterTrans.Boilerplate.Domain.Abstractions.Services;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Web.Api.ObjectResults;

namespace WaterTrans.Boilerplate.Web.Api.Security
{
    public class BearerAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string SchemeName = "Bearer";

        private readonly ILogger<BearerAuthenticationHandler> _logger;
        private readonly IAuthorizeService _authorizeService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private ErrorObjectResult _responseResult;
        private string _responseHeader;

        public BearerAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthorizeService authorizeService,
            IDateTimeProvider dateTimeProvider)
            : base(options, logger, encoder, clock)
        {
            _logger = logger.CreateLogger<BearerAuthenticationHandler>();
            _authorizeService = authorizeService;
            _dateTimeProvider = dateTimeProvider;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return await Task.Run<AuthenticateResult>(() =>
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    _responseHeader = "Bearer realm=\"token_required\"";
                    _responseResult = ErrorObjectResultFactory.NoAuthorizationHeader();
                    return AuthenticateResult.Fail(_responseResult.Error.Message);
                }

                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if (authHeader.Scheme != "Bearer")
                {
                    _responseHeader = "Bearer error=\"invalid_request\"";
                    _responseResult = ErrorObjectResultFactory.InvalidAuthorizationScheme();
                    return AuthenticateResult.Fail(_responseResult.Error.Message);
                }

                try
                {
                    if (authHeader.Parameter == null)
                    {
                        _responseHeader = "Bearer error=\"invalid_token\"";
                        _responseResult = ErrorObjectResultFactory.InvalidAuthorizationToken();
                        return AuthenticateResult.Fail(_responseResult.Error.Message);
                    }

                    var accessToken = _authorizeService.GetAccessToken(authHeader.Parameter);
                    if (accessToken == null || accessToken.Status != AccessTokenStatus.NORMAL)
                    {
                        _responseHeader = "Bearer error=\"invalid_token\"";
                        _responseResult = ErrorObjectResultFactory.InvalidAuthorizationToken();
                        return AuthenticateResult.Fail(_responseResult.Error.Message);
                    }

                    if (accessToken.ExpiryTime < _dateTimeProvider.Now)
                    {
                        _responseHeader = "Bearer error=\"invalid_token\"";
                        _responseResult = ErrorObjectResultFactory.AuthorizationTokenExpired();
                        return AuthenticateResult.Fail(_responseResult.Error.Message);
                    }

                    var result = new List<Claim>();

                    foreach (var role in accessToken.Roles)
                    {
                        result.Add(new Claim(ClaimTypes.Role, role));
                    }

                    foreach (var scope in accessToken.Scopes)
                    {
                        result.Add(new Claim(Scopes.ClaimType, scope));
                    }

                    result.Add(new Claim(ClaimTypes.Name, accessToken.Name));
                    result.Add(new Claim(ClaimTypes.NameIdentifier, accessToken.PrincipalId.ToString()));

                    var identity = new ClaimsIdentity(result.ToArray(), Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    _responseResult = ErrorObjectResultFactory.InternalServerError();
                    return AuthenticateResult.Fail(_responseResult.Error.Message);
                }
            });
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            var options = Context.RequestServices.GetService<IOptions<JsonOptions>>();
            string json = JsonSerializer.Serialize(_responseResult.Value, options.Value.JsonSerializerOptions);

            if (!string.IsNullOrEmpty(_responseHeader))
            {
                Response.Headers["WWW-Authenticate"] = _responseHeader;
            }

            Response.StatusCode = _responseResult.StatusCode.Value;
            Response.ContentType = "application/json";
            await Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(json));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            var options = Context.RequestServices.GetService<IOptions<JsonOptions>>();
            var result = ErrorObjectResultFactory.Forbidden();
            string json = JsonSerializer.Serialize(result.Value, options.Value.JsonSerializerOptions);
            Response.Headers["WWW-Authenticate"] = "Bearer error=\"insufficient_scope\"";
            Response.StatusCode = result.StatusCode.Value;
            Response.ContentType = "application/json";
            await Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(json));
        }
    }
}
