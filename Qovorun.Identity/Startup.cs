using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Qovorun.Identity.Data;
using Qovorun.Identity.Models;

namespace Qovorun.Identity;

public class Startup
{
    protected readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = _configuration.GetValue<string>("DbConnection");

        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddIdentityServer()
            .AddInMemoryApiResources(Configuration.ApiResources)
            .AddInMemoryIdentityResources(Configuration.IdentityResources)
            .AddInMemoryApiScopes(Configuration.ApiScopers)
            .AddInMemoryClients(Configuration.Clients)
            .AddDeveloperSigningCredential();
        
        services.ConfigureApplicationCookie(config =>
        {
            config.Cookie.Name = "Qovorun.Identity.Cookie";
            config.LoginPath = "/Auth/Login";
            config.LogoutPath = "/Auth/Logout";
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}