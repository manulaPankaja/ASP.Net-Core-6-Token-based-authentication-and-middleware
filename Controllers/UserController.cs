using Microsoft.AspNetCore.Mvc;
using Token_based_authentication_and_middleware.DTOs.Requests;
using Token_based_authentication_and_middleware.DTOs.Responses;
using Token_based_authentication_and_middleware.Services.UserService;

namespace Token_based_authentication_and_middleware.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("save")]
        public BaseResponse CreateUser(CreateUserRequest request)
        {
            return userService.CreateUser(request);
        }
    }
}
