using Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFramework
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
            base.OnModelCreating(modelBuilder);

            //User
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(u => u.Id);

                u.HasIndex(u => u.NormalizedUserName).IsUnique();
                u.HasIndex(u => u.NormalizedEmail);

                u.Property(u => u.UserName).HasMaxLength(256);
                u.Property(u => u.NormalizedUserName).HasMaxLength(256);
                u.Property(u => u.Email).HasMaxLength(256);
                u.Property(u => u.NormalizedEmail).HasMaxLength(256);

                //u.HasMany<Role>().WithOne().HasForeignKey(r => r.Users).IsRequired();
                u.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.User.Id).IsRequired();
                u.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.User.Id).IsRequired();
                u.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.User.Id).IsRequired();
            });

            modelBuilder.Entity<UserClaim>(uc => 
            {
                uc.HasKey(uc => uc.Id);
            });

            modelBuilder.Entity<UserLogin>(ul =>
            {
                ul.HasKey(ul => new {ul.LoginProvider, ul.ProviderKey});

                ul.Property(ul => ul.LoginProvider).HasMaxLength(128);
                ul.Property(ul => ul.ProviderKey).HasMaxLength(128);
            });

            modelBuilder.Entity<UserToken>(ut =>
            {
                ut.HasKey(ut => new { ut.User.Id, ut.LoginProvider, ut.Name });

                ut.Property(ut => ut.LoginProvider).HasMaxLength(256);
                ut.Property(ut => ut.Name).HasMaxLength(256);
            });

            //Role
            modelBuilder.Entity<Role>(r =>
            {
                r.HasKey(r => r.Id);

                r.HasIndex(r => r.NormalizedName).IsUnique();

                r.Property(r => r.Name).HasMaxLength(256);
                r.Property(r => r.NormalizedName).HasMaxLength(256);

                r.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.Role.Id).IsRequired();
            });

            modelBuilder.Entity<RoleClaim>(rc =>
            {
                rc.HasKey(rc => rc.Id);
            });

            //Resource
            modelBuilder.Entity<Resource>(r =>
            {
                r.HasKey(r => r.Id);
            });
        }

        #endregion
    }
}
