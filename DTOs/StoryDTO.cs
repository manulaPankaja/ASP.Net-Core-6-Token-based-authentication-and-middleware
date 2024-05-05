using System.ComponentModel.DataAnnotations;

namespace Token_based_authentication_and_middleware.DTOs
{
    public class StoryDTO
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserID { get; set; }
    }
}
