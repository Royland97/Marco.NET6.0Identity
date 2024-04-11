using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFrameworkCore
{
    /// <summary>
    /// Application DbContext
    /// </summary>
    public class ApplicationDbContext: IdentityDbContext<User,Role, string, UserClaim,UserRole, 
                                                         UserLogin, RoleClaim, UserToken>
    {
        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UsersModelBuilder().Configure(modelBuilder);
            SeedRoles(modelBuilder);
        }
        
        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN", Active = true, Description = "Role for the admin user" },
                    new Role() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER", Active = true, Description = "Role for the system user" }
                );
        }

        #endregion
    }
}
