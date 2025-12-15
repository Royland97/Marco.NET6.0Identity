using Core.Domain.Loan;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore.Loan;
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
        public DbSet<Person> Persons { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //new EntityModelBuilder().Configure(modelBuilder);
            new UsersModelBuilder().Configure(modelBuilder);
            new LoanModelBuilder().Configure(modelBuilder);
            SeedData(modelBuilder);
        }
        
        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role() { Name = Role.Admin, ConcurrencyStamp = "1", NormalizedName = "ADMIN", Active = true, Description = "Role for the admin user" },
                    new Role() { Name = Role.User, ConcurrencyStamp = "2", NormalizedName = "USER", Active = true, Description = "Role for the system user" }
                );
            modelBuilder.Entity<User>().HasData(
                    new User() { UserName = "admin", NormalizedUserName = "ADMIN",  Email = "admin@example.com", NormalizedEmail = "ADMIN@EXAMPLE.COM", PhoneNumber = "12344321", Active = true }
                );
        }

        #endregion
    }
}
