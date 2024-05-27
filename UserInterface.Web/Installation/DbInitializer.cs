using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace UserInterface.Web.Installation
{
    /// <summary>
    /// Initialize Db and Populate it with data
    /// </summary>
    public class DbInitializer
    {
        private readonly IInstallResources _installResources;
        private readonly UserManager<User> _userManager;

        public DbInitializer(
            IInstallResources installResources,
            UserManager<User> userManager)
        {
            _installResources = installResources;
            _userManager = userManager;
        }

        public async static Task Initialize(ApplicationDbContext applicationDbContext)
        {
            if( await applicationDbContext.Database.EnsureCreatedAsync())
            {
                //User
                var adminRole = applicationDbContext.Roles.FirstOrDefault(r => r.Name == Role.Admin);
                var adminUser = applicationDbContext.Users.FirstOrDefault(u => u.Email == "admin@example.com");

                if (adminRole == null && adminUser == null)
                    return;

                /*adminUser.PasswordHash = await aux.Method(adminUser);

                applicationDbContext.Users.Update(adminUser);*/
                await applicationDbContext.UserRoles.AddAsync(new UserRole { User = adminUser, Role = adminRole });

                await applicationDbContext.SaveChangesAsync();
            };
        }

        /// <summary>
        /// Install all resources and hash password
        /// </summary>
        /// <returns></returns>
        private async Task<string> Method(User adminUser)
        {
            //Resource
            await _installResources.InstallAsync();

            //Password Hashed
            var hasedPassord = _userManager.PasswordHasher.HashPassword(adminUser, "Admin123");

            return hasedPassord;
        }
    }
}
