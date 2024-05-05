using System.ComponentModel.DataAnnotations;

namespace Token_based_authentication_and_middleware.DTOs.Requests
{
    public class CreateStoryRequest
    {
        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string user_id { get; set; }
    }
}
