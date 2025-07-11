using Microsoft.EntityFrameworkCore;
using Movies.Data;
using System.Diagnostics;

namespace MovieApi.Extensions;
//TODO: Might have to move this to Services to handle the Seed Data
public static class SeedDataExtension
{
    public static async Task SeedDataAsync(this IApplicationBuilder app, bool update = false)
    {

        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<MovieContext>();

        if (update)
        {
            //Auto update and migrate
            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
        }

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
