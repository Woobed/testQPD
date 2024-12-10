using Microsoft.Extensions.Options;
using Serilog;
using testQPD.DaData;
using testQPD.DaData.Interfaces;
using testQPD.Mapping;
using testQPD.Middleware;

namespace testQPD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host.UseSerilog();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.Configure<DaDataSettings>(builder.Configuration.GetSection("DaData"));

            builder.Services.AddHttpClient<IDaDataClient, DaDataClient>((provider, client) =>
            {
                var settings = provider.GetRequiredService<IOptions<DaDataSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Token {settings.ApiKey}");
                client.DefaultRequestHeaders.Add("X-Secret", settings.Secret);
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            
            app.UseMiddleware<MiddlewareException>();
            
            
            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
