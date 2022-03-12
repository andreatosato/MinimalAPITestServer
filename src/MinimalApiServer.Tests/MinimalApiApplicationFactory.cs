using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MinimalApiServer.Databases;

namespace MinimalApiServer.Tests;

public class MinimalApiApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        builder.ConfigureServices(services =>
        {
            var root = new InMemoryDatabaseRoot();
            services.RemoveAll(typeof(DbContextOptions<NotesDbContext>));
            services.AddDbContext<NotesDbContext>(options =>
                options.UseInMemoryDatabase("Testing", root));
        });

        return base.CreateHost(builder);
    }
}
