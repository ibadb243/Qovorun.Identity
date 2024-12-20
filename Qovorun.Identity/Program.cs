using Qovorun.Identity.Data;

namespace Qovorun.Identity;

public class Program2
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using(var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var context = serviceProvider.GetRequiredService<AuthDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger>();
                logger.LogError(ex, "An error occurred while app initialization");
            }
        }

        host.Run();
    }
    
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
            });
}