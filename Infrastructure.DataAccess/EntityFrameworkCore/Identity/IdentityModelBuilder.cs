using Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityFrameworkCore.Identity
{
    /// <summary>
    /// Identity Model Configuration
    /// </summary>
    public class IdentityModelBuilder
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);

                b.HasIndex(u => u.NormalizedUserName).IsUnique();
                b.HasIndex(u => u.NormalizedEmail);

                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                b.HasMany(u => u.UserClaims);
                b.HasMany(u => u.UserLogins);
                b.HasMany(u => u.UserTokens);
                b.HasMany(u => u.Roles).WithMany(r => r.Users).UsingEntity("AspNetUserRoles");

                /*b.HasMany(u => u.UserClaims).WithOne(uc => uc.User).HasForeignKey("UserId").IsRequired();
                b.HasMany(u => u.UserLogins).WithOne(ul => ul.User).HasForeignKey("UserId").IsRequired();
                b.HasMany(u => u.UserTokens).WithOne(ut => ut.User).HasForeignKey("UserId").IsRequired();*/
            });

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasKey(uc => uc.Id);
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

                b.Property(ul => ul.LoginProvider).HasMaxLength(128);
                b.Property(ul => ul.ProviderKey).HasMaxLength(128);
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.HasKey(ut => new { ut.LoginProvider, ut.Name });

                b.Property(ut => ut.LoginProvider).HasMaxLength(256);
                b.Property(ut => ut.Name).HasMaxLength(256);
            });

            //Role
            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);

                b.HasIndex(r => r.NormalizedName).IsUnique();

                b.Property(r => r.Name).HasMaxLength(256);
                b.Property(r => r.NormalizedName).HasMaxLength(256);

                b.HasMany(r => r.Users).WithMany(r => r.Roles).UsingEntity("AspNetUserRoles");
                b.HasMany(r => r.Resources).WithMany(r => r.Roles).UsingEntity("AspNetResourceRoles");
                b.HasMany(r => r.RoleClaims);
            });

            modelBuilder.Entity<RoleClaim>(b =>
            {
                b.HasKey(rc => rc.Id);
            });

            //Resource
            modelBuilder.Entity<Resource>(b =>
            {
                b.HasKey(r => r.Id);

                b.HasMany(r => r.Roles).WithMany(r => r.Resources).UsingEntity("AspNetResourceRoles");
            });
        }
    }
}
