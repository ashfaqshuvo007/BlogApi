using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models
{
    public enum Role
    {
        User,
        Admin
    }
    public class User
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string UserName { get; set; }

        public required string? Password { get; set; }

        public Role Role { get; set; } = Role.User;

        public ICollection<Blog> Blogs { get; } = new List<Blog>();

    }
}
