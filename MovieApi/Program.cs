using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext") ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));

// Add services to the container.

builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// builder.Services.AddSwaggerGen(opt =>
//{
//    opt.EnableAnnotations();
//});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MapperProfile>();
});

builder.Services.AddOpenApi();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/openapi/v1.json", "v1");
    //});
    //Set to True to SeedData
    await app.SeedDataAsync(update: true);

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
