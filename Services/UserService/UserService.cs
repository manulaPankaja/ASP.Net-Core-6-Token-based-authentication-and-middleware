using Token_based_authentication_and_middleware.DTOs.Requests;
using Token_based_authentication_and_middleware.DTOs.Responses;
using Token_based_authentication_and_middleware.Models;

namespace Token_based_authentication_and_middleware.Services.UserService
{
    public class UserService : IUserService
    {
        //variable to store application db context
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            //get db context through DI
            context = applicationDbContext;
        }

        public BaseResponse CreateUser(CreateUserRequest request)
        {
            BaseResponse response;

            try
            {
                //create new instance of user model
                UserModel newUser = new UserModel();
                newUser.username = request.username;
                newUser.email = request.email;
                newUser.password = Supports.MD5HashGenerator.GenerateMD5(request.password);
                newUser.first_name = request.first_name;
                newUser.last_name = request.last_name;

                using (context)
                {
                    context.Add(newUser);
                    context.SaveChanges();
                }

                response = new BaseResponse()
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new user" }
                };

                return response;
            }catch (Exception ex)
            {
                response = new BaseResponse()
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };

                return response;
            }
        }
    }

   

}
