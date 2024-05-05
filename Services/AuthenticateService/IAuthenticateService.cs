using Token_based_authentication_and_middleware.DTOs.Requests;
using Token_based_authentication_and_middleware.DTOs.Responses;

namespace Token_based_authentication_and_middleware.Services.AuthenticateService
{
    public interface IAuthenticateService
    {
        BaseResponse Authenticate(AuthenticateRequest request);

    }
}
