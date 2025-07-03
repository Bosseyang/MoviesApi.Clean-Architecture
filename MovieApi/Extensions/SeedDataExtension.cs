using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using System.Diagnostics;

namespace MovieApi.Extensions;

public static class SeedDataExtension
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<MovieContext>();

        //Auto update and migrate
        //await context.Database.EnsureDeletedAsync();
        //await context.Database.MigrateAsync();

        try
        {
            await SeedData.InitAsync(context);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            throw;
        }
    }
}
