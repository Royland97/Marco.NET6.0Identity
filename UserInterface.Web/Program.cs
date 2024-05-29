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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Core.DataAccess.IRepository.Loan;
using Infrastructure.DataAccess.Repository.Loan;
using Infrastructure.Services.AccessExternalApi;

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

            builder.Configuration.AddJsonFile("appsettings.json", true, true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
            builder.Configuration.AddEnvironmentVariables();
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);

            #region Services

            //DBContext
            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"), b => b.MigrationsAssembly("UserInterface.Web"));
                    //options.UseSqlServer(builder.Configuration.GetConnectionString("DockerSqlServer"), b => b.MigrationsAssembly("UserInterface.Web"));
                    //options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"), b => b.MigrationsAssembly("UserInterface.Web"));
                }
            );
                
            //Authentication and Authorization
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ActivePolicy", policy => policy.RequireClaim("ActiveUser", "True"));
                options.AddPolicy("ResourceAuthorize", policy => policy.Requirements.Add(new ResourceAuthorizationRequirement()));
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ForwardDefault = JwtBearerDefaults.AuthenticationScheme;
            });

            //Identity
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            builder.Services.Configure<IdentityOptions>(options =>
            {
                //SignIn settings
                //options.SignIn.RequireConfirmedEmail = true;

                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
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
                    });
            });

            builder.Services.AddAutoMapper(typeof(BaseProfile));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            //Swagger
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API .NET 6.0" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Jwt",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] { } 
                    }
                });
            });

            //Dependencies
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            builder.Services.AddScoped(typeof(IResourceRepository), typeof(ResourceRepository));
            builder.Services.AddScoped(typeof(IInstallResources), typeof(InstallResources));
            builder.Services.AddScoped(typeof(IEndPointServices), typeof(EndPointServices));
            builder.Services.AddScoped(typeof(ITokenServices), typeof(TokenServices));
            builder.Services.AddScoped(typeof(IHostNameServices), typeof(HostNameServices));
            builder.Services.AddScoped(typeof(IPersonRepository), typeof(PersonRepository));
            builder.Services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));

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

            app.UseSwagger();
            app.UseSwaggerUI();

            /*
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Static")),
                RequestPath = "/Static"
            });*/

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