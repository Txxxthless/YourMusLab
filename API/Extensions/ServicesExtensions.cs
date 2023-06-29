using API.Helpers;
using DAL;
using DAL.Interface;
using DAL.Repository;
using DAL.Service;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAppServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMapper<Track>, TrackMapper>();
            services.AddScoped<IMapper<Album>, AlbumMapper>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddSingleton<IConnectionMultiplexer>(cofig =>
            {
                var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options); 
            });
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    policy =>
                    {
                        policy
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("https://localhost:4300");
                    }
                );
            });

            return services;
        }
    }
}
