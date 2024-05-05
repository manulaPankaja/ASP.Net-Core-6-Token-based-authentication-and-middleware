using Microsoft.AspNetCore.Mvc;
using Token_based_authentication_and_middleware.DTOs.Requests;
using Token_based_authentication_and_middleware.DTOs.Responses;
using Token_based_authentication_and_middleware.Services.AuthenticateService;
using Token_based_authentication_and_middleware.Services.UserService;

namespace Token_based_authentication_and_middleware.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }

        [HttpPost("authenticate")]
        public BaseResponse CreAuthenticateateUser(AuthenticateRequest request)
        {
            return authenticateService.Authenticate(request);
        }
    }
}
