using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Token_based_authentication_and_middleware.DTOs.Responses;
using Token_based_authentication_and_middleware.Helpers.Utils;

namespace Token_based_authentication_and_middleware.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            string? token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];

            if (token == null)
            {
                //check incoming request is from a enabled unauthorized route
                if (IsEnabledUnautherizedRoute(httpContext)){
                    return _next(httpContext);
                }

                BaseResponse response = new BaseResponse
                {
                    status_code = StatusCodes.Status401Unauthorized,
                    data = new { message = "Unauthorized" }
                };
                httpContext.Response.StatusCode = response.status_code;
                httpContext.Response.ContentType = "application/json";
                return httpContext.Response.WriteAsJsonAsync(response);
            }
            else
            {
                if (JwtUtils.ValidateJwtToken(token))
                {
                    return _next(httpContext);
                }
                else
                {
                    BaseResponse response = new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = new { message = "Unauthorized" }
                    };
                    httpContext.Response.StatusCode = response.status_code;
                    httpContext.Response.ContentType = "application/json";
                    return httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }

        private bool IsEnabledUnautherizedRoute(HttpContext httpContext)
        {
            List<string> enabledRoutes = new List<string>()
            {
                "api/user/save",
                "/api/auth/authenticate"
            };

            bool IsEnabledUnautherizedRoute = false;

            if(httpContext.Request.Path.Value is not null)
            {
                IsEnabledUnautherizedRoute = enabledRoutes.Contains(httpContext.Request.Path.Value);
            }

            return IsEnabledUnautherizedRoute;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
