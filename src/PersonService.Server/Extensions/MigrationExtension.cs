using Microsoft.EntityFrameworkCore;
using PersonService.Database.Context;

namespace PersonService.Server.Extensions;

public static class MigrationExtension
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<PersonServiceContext>();
        
        context.Database.Migrate();

        return host;
    }
}