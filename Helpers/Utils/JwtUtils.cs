using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Token_based_authentication_and_middleware.Models;

namespace Token_based_authentication_and_middleware.Helpers.Utils
{
    public class JwtUtils
    {
        static string secret = "sdhs246432hdsgHGDhh62636";

        public static string GenerateJwtToken(UserModel user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secret);

            //token claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim("user_id",user.id.ToString()),
                new Claim("username",user.username),
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }

        public static bool ValidateJwtToken(string jwt)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(secret);

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
                JwtSecurityToken validatedJWT = (JwtSecurityToken)validatedToken;

                //get claims
                long userId = long.Parse(validatedJWT.Claims.First(claim => claim.Type == "user_id").Value);

                using(ApplicationDbContext dbContext =new ApplicationDbContext())
                {
                    UserModel? user = dbContext.Users.FirstOrDefault(u => u.id == userId);

                    if (user == null)
                    {
                        return false;
                    }
                    else
                    {
                        //token is valid and latest token
                        return true;
                    }
                }
            }catch (Exception ex) {
                return false;
            }
        }
    }
}
