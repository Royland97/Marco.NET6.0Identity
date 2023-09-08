using Microsoft.Extensions.FileProviders;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Core.DataAccess.IRepository.Users;
using Infrastructure.DataAccess.Repository.Users;
using UserInterface.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using UserInterface.Web.Authorization;
using UserInterface.Web.Installation;

namespace UserInterface.Web
{
    /// <summary>
    /// Class that is the entry point of the application.
    /// </summary>
    public class Program
    {
        #region Methods

        /// <summary>
        /// Entry point to launch the application.
        /// </summary>
        /// 
        /// <param name="args">Command arguments to start the application</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseKestrel(opt => opt.AddServerHeader = false);
            builder.Configuration.AddJsonFile("appsettings.json", true, true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
            builder.Configuration.AddJsonFile("settings.json", true, true);
            builder.Configuration.AddEnvironmentVariables();
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);

            #region Services

            //DBContext
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

            //Authentication and Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ResourceAuthorize", policy => policy.Requirements.Add(new ResourceAuthorizationRequirement()));
            });

            //Identity

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAllCorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowAnyOrigin();
                        //policy.AllowCredentials();
                    });
            });

            builder.Services.AddAutoMapper(typeof(BaseProfile));

            builder.Services.AddControllersWithViews();

            //Dependencies
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            builder.Services.AddScoped(typeof(IResourceRepository), typeof(ResourceRepository));
            builder.Services.AddScoped(typeof(IAuthorizationHandler), typeof(ResourceAuthorizationHandler));

            #endregion

            #region Middleware

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Static")),
                RequestPath = "/Static"
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAllCorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/");

            app.Run();

            #endregion
        }

        #endregion
    }
}