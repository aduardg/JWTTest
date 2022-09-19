using Microsoft.EntityFrameworkCore;
using TestJwt.Models;

namespace TestJwt.Database
{
    public class DbEF: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RoleUser> Roles { get; set; }
        public DbEF()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=Eduard;Password=527225");
        }
    }
}
