
using Atlas.Repository.AccountsRepository;
using Atlas.Repository.ApplicationsRepository;
using Atlas.Repository.AtlastDb;
using Atlas.Repository.CardsRepository;
using Atlas.Services.AccountsService;
using Atlas.Services.ApplicationsService;
using Atlas.Services.CardsService;
using Atlas.Services.IDNowService;
using Atlas.Services.RefinitivWorldCheckService;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Explicit ports for HTTP and HTTPS so Swagger UI can be reached at a known address
            const int httpPort = 5005;
            const int httpsPort = 5006;
            builder.WebHost.UseUrls($"http://localhost:{httpPort}", $"https://localhost:{httpsPort}");

            builder.Services.AddDbContext<AtlasDbContext>(options =>
                options.UseInMemoryDatabase("AtlasDatabase"));

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IApplicationsService, ApplicationService>();
            builder.Services.AddScoped<IAccountsService, AccountsService>();
            builder.Services.AddScoped<ICardsService, CardsService>();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddScoped<IIDNowService, MockIDNowService>();
                builder.Services.AddScoped<IRefinitivWorldCheckService, MockRefinitivWorldCheckService>();
            }
            else
            {
                builder.Services.AddScoped<IIDNowService, IDNowService>();
                builder.Services.AddHttpClient<IIDNowService, IDNowService>();
                builder.Services.AddScoped<IRefinitivWorldCheckService, RefinitivWorldCheckService>();
                builder.Services.AddHttpClient<IRefinitivWorldCheckService, RefinitivWorldCheckService>();
            }

            builder.Services.AddScoped<IApplicationsRepository, ApplicationsRepository>();
            builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
            builder.Services.AddScoped<ICardsRepository, CardsRepository>();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    // Expose Swagger UI at the application root for convenience
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Atlas API v1");
                    c.RoutePrefix = string.Empty;
                });

                // Try to automatically open the Swagger UI in the default browser after the app starts
                var launchUrl = $"https://localhost:{httpsPort}/";
                app.Lifetime.ApplicationStarted.Register(() =>
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = launchUrl,
                            UseShellExecute = true
                        });
                    }
                    catch
                    {
                        // Best-effort: ignore any failures to open a browser
                    }
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

