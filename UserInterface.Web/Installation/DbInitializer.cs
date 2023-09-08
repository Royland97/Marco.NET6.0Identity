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
        private readonly InstallResources _installResources;
        private readonly IResourceRepository _resourceRepository;

        public DbInitializer(InstallResources installResources, IResourceRepository resourceRepository)
        {
            _installResources = installResources;
            _resourceRepository = resourceRepository;
        }

        public static void Initialize(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.EnsureCreated();

            //await _installResources.InstallAsync();

            //Role
            var adminRole = new Role
            {
                Active = true,
                IsSystemRole = true,
                Name = Role.Administrator,
                NormalizedName = Role.Administrator.ToUpper(),
                Description = $"Role for the {Role.Administrator} user."
            };

            applicationDbContext.Roles.Add(adminRole);

            applicationDbContext.SaveChanges();

            //User

            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                Email = "admin@test.cu",
                PhoneNumber = "1231244234",
                Active = true
            };

            adminUser.Roles.Add(adminRole);

            applicationDbContext.Users.Add(adminUser);
            applicationDbContext.SaveChanges();
        }
    }
}
