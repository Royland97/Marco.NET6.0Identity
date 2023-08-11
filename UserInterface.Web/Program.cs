using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Core.DataAccess.Users;
using Infrastructure.DataAccess.Users;
using Infrastructure.Services.Users.Services;
using Infrastructure.Services.Users.IServices;
using Infrastructure.Services;

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
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("Marco.NET6.0")));
            
            //Authentication and Authoritation
            builder.Services.AddAuthenticationCore(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            
            builder.Services.Configure<IdentityOptions>(options =>
            {
                //Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                //Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            builder.Services.Configure<PasswordHasherOptions>(options =>
            {
                options.IterationCount = 12000;
            });

            builder.Services.AddTransient<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>().HttpContext.User);
            
            //Cookies
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                /*
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api")
                    && context.Response.StatusCode == StatusCodes.Status200OK)
                    {
                        context.Response.Clear();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.FromResult<object>(null);
                    }
                    context.Response.Redirect(context.RedirectUri);
                    return Task.FromResult<object>(null);
                };

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;*/
            });

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAllPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowAnyOrigin();
                    });
            });

            builder.Services.AddAutoMapper(typeof(BaseProfile));
            builder.Services.AddControllersWithViews();

            //Dependencies
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(IUserServices), typeof(UserServices));
            builder.Services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            builder.Services.AddScoped(typeof(IRoleServices), typeof(RoleServices));
            builder.Services.AddScoped(typeof(IResourceRepository), typeof(ResourceRepository));
            builder.Services.AddScoped(typeof(IResourceServices), typeof(ResourceServices));

            #endregion

            #region Middleware

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                //DbInitializer.Initialize(context);
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
            app.UseCors("AllowAllPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/");

            //app.MapRazorPages();

            app.Run();

            #endregion
        }

        #endregion
    }
}