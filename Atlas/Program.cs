
using Atlas.Repository.AccountsRepository;
using Atlas.Repository.ApplicationsRepository;
using Atlas.Repository.AtlastDb;
using Atlas.Repository.CardsRepository;
using Atlas.Services.AccountsService;
using Atlas.Services.ApplicationsService;
using Atlas.Services.CardsService;
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
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

