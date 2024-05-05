using System.ComponentModel.DataAnnotations;

namespace Token_based_authentication_and_middleware.DTOs.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
