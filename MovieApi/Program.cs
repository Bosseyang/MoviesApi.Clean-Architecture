using Microsoft.EntityFrameworkCore;
using MovieApi.Extensions;
using Movies.Core.DomainContracts;
using Movies.Data;
using Movies.Data.Repositories;
using Movies.Services;
using Movies.Services.Contracts;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext") ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));

// Add services to the container.

builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
            /*.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)*/; 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// builder.Services.AddSwaggerGen(opt =>
//{
//    opt.EnableAnnotations();
//});
//Might have to move this out to ServiceExtensions later.
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
    //Set to true to update and Seed Data, false to skip
    await app.SeedDataAsync(update: false);

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
