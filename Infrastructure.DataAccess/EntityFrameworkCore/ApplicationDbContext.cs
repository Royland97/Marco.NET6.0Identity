using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFrameworkCore
{
    /// <summary>
    /// Application DbContext
    /// </summary>
    public class ApplicationDbContext: DbContext
    {
        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<UserClaim> Claims { get; set; }
        public DbSet<UserLogin> Logins { get; set; }
        public DbSet<UserToken> Tokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new IdentityModelBuilder().Configure(modelBuilder);
        }

        #endregion
    }
}
