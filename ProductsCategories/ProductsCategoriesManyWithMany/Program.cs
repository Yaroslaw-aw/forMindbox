using Microsoft.EntityFrameworkCore;
using ProductsCategoriesManyWithMany.Caching;
using ProductsCategoriesManyWithMany.Db;
using ProductsCategoriesManyWithMany.Mapping;
using ProductsCategoriesManyWithMany.Repositories;

namespace ProductsCategoriesManyWithMany
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            
            

            builder.Services.AddScoped<ITestProductCategory, TestProductCategory>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                string server = "redishost";
                string port = "6379";
                string cnstring = $"{server}:{port}";
                options.Configuration = cnstring;
            });

            builder.Services.AddMemoryCache(options =>
            {
                options.TrackStatistics = true;
                options.TrackLinkedCacheEntries = true;
            });

            builder.Services.AddSingleton<Redis>();
            builder.Services.AddLogging();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.LogTo(Console.WriteLine).UseNpgsql(builder.Configuration.GetConnectionString("db"));
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
