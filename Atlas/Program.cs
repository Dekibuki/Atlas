
using Atlas.Repository.ApplicationsRepository;
using Atlas.Repository.AtlastDb;
using Atlas.Services.ApplicationsService;
using Atlas.Services.IDNowService;
using Atlas.Services.RefinitivWorldCheckService;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AtlasDbContext>(options =>
                options.UseInMemoryDatabase("AtlasDatabase"));

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IApplicationsService, ApplicationService>();

            builder.Services.AddScoped<IIDNowService, IDNowService>();
            builder.Services.AddHttpClient<IIDNowService, IDNowService>();

            builder.Services.AddScoped<IRefinitivWorldCheckService, RefinitivWorldCheckService>();
            builder.Services.AddHttpClient<IRefinitivWorldCheckService, RefinitivWorldCheckService>();

            builder.Services.AddScoped<IApplicationsRepository, ApplicationsRepository>();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

