using Bme.Swlab1.Rest.Dal;
using Bme.Swlab1.Rest.Services;

using Microsoft.EntityFrameworkCore;

namespace Bme.Swlab1.Rest;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<TasksDbContext>(options => options.UseSqlite("Data Source=tasks.db"));
        builder.Services.AddScoped<IStatusService, StatusService>();
        builder.Services.AddScoped<ITaskService, TaskService>();

        var app = builder.Build();

        using (var serviceScope = app.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<TasksDbContext>();
            context.Database.EnsureCreated();
        }

        app.MapControllers();

        app.Run();
    }
}