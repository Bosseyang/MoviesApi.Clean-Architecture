
using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;
using System.Globalization;

namespace MovieApi.Data;
public class SeedData
{
    private static Faker faker = new Faker("sv");
    internal static async Task InitAsync(MovieContext context)
    {
        //If there's data already, return
        if (await context.Movies.AnyAsync()) return;

        var movies = GenerateMovies(50);
        var actors = GenerateActors(15);

    }

    private static List<Movie> GenerateMovies(int numberOfMovies)
    {
        var movies = new List<Movie>();
        Random rand = new Random();
        var genreList = new List<string> { "Action", "Romance", "Drama", "Thriller", "Horror", 
            "Comedy", "Western", "Fantasy", "Science Fiction", "Documentary" +
            "Musical", "Crime", "Animation", "Sport", "Historical"};
        var languageList = new List<string> { "Swedish", "English", "Spanish", "French", "German", "Italian"};


        for (int i = 0; i < numberOfMovies; i++)
        {
            var titleSize = rand.Next(2, 8);
            var titleWords = faker.Lorem.Words(titleSize);
            var titleJoin = string.Join(" ", titleWords);
            var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(titleJoin);

            var year = rand.Next(1900, 2025);
            var genre = genreList[rand.Next(0, genreList.Count)];
            //Duration in minutes
            var duration = rand.Next(45, 240);
            var budget = rand.Next(50000, 500000000);
            var movie = new Movie
            {
                Title = title,
                Year = year,
                Genre = genre,
                Duration = duration,
                MovieDetails = new MovieDetails
                {
                    Synopsis = faker.Lorem.Sentences(),
                    Language = languageList[rand.Next(0, languageList.Count)],
                    Budget = budget,
                },
                //TODO: Add reviews 1:M and fix N:M between movie and actor

            };
            movies.Add(movie);
        }
        return movies;
    }

    private static List<Actor> GenerateActors(int numberOfActors)
    {
        var actors = new List<Actor>();
        Random rand = new Random();

        for (int i = 0; i < numberOfActors; i++)
        {
            var name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Name.FullName());
            var birthYear = rand.Next(1800, 2015);
            var actor = new Actor { Name = name, BirthYear = birthYear };
            actors.Add(actor);
            
        }
        return actors;
    }
    
}
