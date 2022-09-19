using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestJwt.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Refresh_Token { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? Created_RefreshToken { get; set; }
        public List<RoleUser> roles { get; set; } = new List<RoleUser>();
    }
}
