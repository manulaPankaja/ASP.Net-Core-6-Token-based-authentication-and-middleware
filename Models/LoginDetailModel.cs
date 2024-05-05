using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Token_based_authentication_and_middleware.Models
{
    [Table("login_detail")]
    public class LoginDetailModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        public long user_id { get; set; }

        [Required]
        public string token {  get; set; }
    }
}
