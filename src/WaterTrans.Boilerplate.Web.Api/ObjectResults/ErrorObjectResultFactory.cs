using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Net.Mime;
using WaterTrans.Boilerplate.CrossCuttingConcerns.ExtensionMethods;
using WaterTrans.Boilerplate.Web.Api.ResponseObjects;
using WaterTrans.Boilerplate.Web.Resources;

namespace WaterTrans.Boilerplate.Web.Api.ObjectResults
{
    public static class ErrorObjectResultFactory
    {
        public static ErrorObjectResult ValidationError(ModelStateDictionary modelState)
        {
            var errors = new List<Error>();

            foreach (var keyValue in modelState)
            {
                foreach (var error in keyValue.Value.Errors)
                {
                    errors.Add(new Error
                    {
                        Code = ErrorCodes.ValidationErrorDetail,
                        Message = error.ErrorMessage,
                        Target = keyValue.Key.ToCamelCase(),
                    });
                }
            }

            return ValidationError(errors);
        }

        public static ErrorObjectResult ValidationError(Error error)
        {
            return ValidationError(new List<Error>() { error });
        }

        public static ErrorObjectResult ValidationError(List<Error> errors)
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.ValidationError,
                Message = ErrorMessages.ErrorResultValidationError,
                Details = errors,
            });
            result.ContentTypes.Add(MediaTypeNames.Application.Json);
            return result;
        }

        public static ErrorObjectResult ValidationErrorDetail(string message, string target)
        {
            return ValidationError(new Error
            {
                Code = ErrorCodes.ValidationErrorDetail,
                Message = message,
                Target = target,
            });
        }

        public static ErrorObjectResult BadRequest()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.BadRequest,
                Message = ErrorMessages.ErrorResultBadRequest,
            });
            result.StatusCode = StatusCodes.Status400BadRequest;
            return result;
        }

        public static ErrorObjectResult NoAuthorizationHeader()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.NoAuthorizationHeader,
                Message = ErrorMessages.ErrorResultNoAuthorizationHeader,
            });
            result.StatusCode = StatusCodes.Status401Unauthorized;
            return result;
        }

        public static ErrorObjectResult InvalidClient()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.InvalidClient,
                Message = ErrorMessages.ErrorResultInvalidClient,
            });
            result.StatusCode = StatusCodes.Status401Unauthorized;
            return result;
        }

        public static ErrorObjectResult InvalidCode()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.InvalidCode,
                Message = ErrorMessages.ErrorResultInvalidCode,
            });
            result.StatusCode = StatusCodes.Status401Unauthorized;
            return result;
        }

        public static ErrorObjectResult InvalidRefreshToken()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.InvalidRefreshToken,
                Message = ErrorMessages.ErrorResultInvalidRefreshToken,
            });
            result.StatusCode = StatusCodes.Status401Unauthorized;
            return result;
        }

        public static ErrorObjectResult InvalidGrantType()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.InvalidGrantType,
                Message = ErrorMessages.ErrorResultInvalidGrantType,
            });
            result.StatusCode = StatusCodes.Status400BadRequest;
            return result;
        }

        public static ErrorObjectResult InvalidScope()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.InvalidScope,
                Message = ErrorMessages.ErrorResultInvalidScope,
            });
            result.StatusCode = StatusCodes.Status400BadRequest;
            return result;
        }

        public static ErrorObjectResult InvalidAuthorizationScheme()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.InvalidAuthorizationScheme,
                Message = ErrorMessages.ErrorResultInvalidAuthorizationScheme,
            });
            result.StatusCode = StatusCodes.Status400BadRequest;
            return result;
        }

        public static ErrorObjectResult InvalidAuthorizationToken()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.InvalidAuthorizationToken,
                Message = ErrorMessages.ErrorResultInvalidAuthorizationToken,
            });
            result.StatusCode = StatusCodes.Status401Unauthorized;
            return result;
        }

        public static ErrorObjectResult AuthorizationTokenExpired()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.AuthorizationTokenExpired,
                Message = ErrorMessages.ErrorResultAuthorizationTokenExpired,
            });
            result.StatusCode = StatusCodes.Status401Unauthorized;
            return result;
        }

        public static ErrorObjectResult Unauthorized()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.Unauthorized,
                Message = ErrorMessages.ErrorResultUnauthorized,
            });
            result.StatusCode = StatusCodes.Status401Unauthorized;
            return result;
        }

        public static ErrorObjectResult Forbidden()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.Forbidden,
                Message = ErrorMessages.ErrorResultForbidden,
            });
            result.StatusCode = StatusCodes.Status403Forbidden;
            return result;
        }

        public static ErrorObjectResult NotFound()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.NotFound,
                Message = ErrorMessages.ErrorResultNotFound,
            });
            result.StatusCode = StatusCodes.Status404NotFound;
            return result;
        }

        public static ErrorObjectResult InternalServerError()
        {
            var result = new ErrorObjectResult(new Error()
            {
                Code = ErrorCodes.InternalServerError,
                Message = ErrorMessages.ErrorResultInternalServerError,
            });
            result.StatusCode = StatusCodes.Status500InternalServerError;
            return result;
        }
    }
}
