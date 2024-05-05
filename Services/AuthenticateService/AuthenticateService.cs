using Token_based_authentication_and_middleware.DTOs.Requests;
using Token_based_authentication_and_middleware.DTOs.Responses;
using Token_based_authentication_and_middleware.Helpers.Utils;
using Token_based_authentication_and_middleware.Models;

namespace Token_based_authentication_and_middleware.Services.AuthenticateService
{
    public class AuthenticateService : IAuthenticateService
    {
        //variable to store application db context
        private readonly ApplicationDbContext context;

        public AuthenticateService(ApplicationDbContext applicationDbContext)
        {
            //get db context through DI
            context = applicationDbContext;
        }
        public BaseResponse Authenticate(AuthenticateRequest request)
        {
            try
            {
                UserModel? user = context.Users.Where(u => u.username == request.username).FirstOrDefault();
                if (user == null)
                {
                    return new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = new { message = "Invalid username or password" }
                    };
                }

                //get password in MD5
                string md5Password = Supports.MD5HashGenerator.GenerateMD5(request.password);

                //match password
                if (user.password == md5Password)
                {
                    //get jwt
                    string jwt = JwtUtils.GenerateJwtToken(user);

                    //save token in login details
                    LoginDetailModel? loginDetail = context.loginDetails.Where(ld => ld.user_id == user.id).FirstOrDefault();

                    if (loginDetail == null)
                    {
                        loginDetail = new LoginDetailModel();
                        loginDetail.user_id = user.id;
                        loginDetail.token = jwt;

                        context.loginDetails.Add(loginDetail);
                    }
                    else
                    {
                        loginDetail.token = jwt;
                    }
                    context.SaveChanges();

                    return new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { token = jwt }
                    };
                }
                else
                {
                    return new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = new { message = "Invalid username or password" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { mesage = ex.Message }
                };
            }
        }
    }
}
