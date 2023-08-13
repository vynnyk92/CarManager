using CarManager.Extensions;
using CarManager.Services;
using CarManager.Shared;

namespace CarManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configuration
            var configuration = builder.Configuration;

            builder.Services.Configure<AppSettings>(
                builder.Configuration.GetSection(nameof(AppSettings)));

            builder.Services.Configure<LocalstackSettings>(
                builder.Configuration.GetSection(nameof(LocalstackSettings)));

            // Add services to the container.
            builder.Services.AddAutoMapper();
            builder.Services.AddSingleton<ICarProvider, CarProvider>();
            builder.Services.AddDynamoDb(configuration);

            builder.Services.AddHealthChecks().AddCheck<SampleHealthCheckWithArgs>("/health/check"); ;

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.MapHealthChecks("/health/check");
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}