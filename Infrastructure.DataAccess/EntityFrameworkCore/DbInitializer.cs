using Core.Domain.Users;

namespace Infrastructure.DataAccess.EntityFrameworkCore
{
    /// <summary>
    /// Initialize Db and Populate it with data
    /// </summary>
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.EnsureCreated();

            //Role
            var adminRole = new Role
            {
                Active = true,
                IsSystemRole = true,
                Name = Role.Administrator,
                NormalizedName = Role.Administrator.ToUpper(),
                Description = $"Role para el usuario {Role.Administrator}."
            };
            applicationDbContext.Roles.Add(adminRole);

            applicationDbContext.SaveChanges();

            //User

            var adminUser = new User
            {
                UserGuid = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "admin@test.cu",
                NormalizedEmail = "admin@test.cu".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "1231244234",
                PhoneNumberConfirmed = true,
                Active = true
            };

            applicationDbContext.Users.Add(adminUser);

            applicationDbContext.SaveChanges();
        }
    }
}
