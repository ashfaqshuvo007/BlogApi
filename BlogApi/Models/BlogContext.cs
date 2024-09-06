using Microsoft.EntityFrameworkCore;


namespace BlogApi.Models
{
    public class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options)
    {
        public DbSet<Blog> Blogs { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

    }
}
