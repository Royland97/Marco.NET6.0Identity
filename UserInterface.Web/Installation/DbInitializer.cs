using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace UserInterface.Web.Installation
{
    /// <summary>
    /// Initialize Db and Populate it with data
    /// </summary>
    public class DbInitializer
    {
        private readonly IInstallResources _installResources;

        public DbInitializer(
            IInstallResources installResources)
        {
            _installResources = installResources;
        }

        public async static Task Initialize(ApplicationDbContext applicationDbContext)
        {
            if( await applicationDbContext.Database.EnsureCreatedAsync())
            {
                //User
                var adminRole = applicationDbContext.Roles.FirstOrDefault(r => r.Name == Role.Admin);

                if (adminRole == null)
                    return;

                var adminUser = new User
                {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@test.cu",
                    NormalizedEmail = "ADMIN@TEST.CU",
                    PhoneNumber = "1231244234",
                    Active = true
                };

                await applicationDbContext.Users.AddAsync(adminUser);
                await applicationDbContext.UserRoles.AddAsync(new UserRole { User = adminUser, Role = adminRole });

                //Resource
                //await _installResources.InstallAsync();

                await applicationDbContext.SaveChangesAsync();
            };
        }
    }
}
