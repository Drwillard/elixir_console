using ElixirProject_Willard.Data;
using ElixirProject_Willard.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace ElixirProject_Willard;

public class Program
{
    public static async Task Main(string[] args)
    {

        var host = Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
           })
           .ConfigureServices((context, services) =>
             {
                 services.AddScoped<ISurveyService, SurveyService>();
                 services.AddScoped<QuestionRepository>();
                 services.AddScoped<AnswerRepository>();
                 services.AddScoped<PersonRepository>();

                 // Retrieve the connection string from appsettings.json
                 var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                 // Configure the DbContext with the connection string
                 services.AddDbContext<AppDbContext>(options =>
                 {
                     options.UseSqlite(connectionString);
                 });
             })

           .Build();

        // ensure db is set up and seeded with questions
        using (var scope = host.Services.CreateScope())
        {
            await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
            dbContext.Database.EnsureCreated();

        }

        // run the survey to infinity
        var service = host.Services.GetRequiredService<ISurveyService>();
        while (true)
        {
            service.Run();
        }

    }
}
