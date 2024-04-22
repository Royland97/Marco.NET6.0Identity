using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using UserInterface.Web.ViewModels;
using UserInterface.Web.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Core.DataAccess.IRepository.Users;
using Infrastructure.DataAccess.Repository.Users;
using UserInterface.Web.Installation;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Core.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

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
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseKestrel(opt => opt.AddServerHeader = false);
            builder.Configuration.AddJsonFile("appsettings.json", true, true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
            builder.Configuration.AddEnvironmentVariables();
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);

            #region Services

            //DBContext
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"), b => b.MigrationsAssembly("UserInterface.Web")));

            //Authentication and Authorization
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            /*
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
                options.AddPolicy("ResourceAuthorize", policy => policy.Requirements.Add(new ResourceAuthorizationRequirement()));
            });*/

            //Identity
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            //Dependencies
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            builder.Services.AddScoped(typeof(IResourceRepository), typeof(ResourceRepository));

            #endregion

            #region Middleware

            var app = builder.Build();
            
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    await DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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

            app.MapControllers();
            
            app.Run();

            #endregion
        }

        #endregion
    }
}