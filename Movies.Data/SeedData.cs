using Bogus;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Entities;
using System.Globalization;

namespace Movies.Data;
public class SeedData
{
    private static Faker faker = new Faker("sv");
    public static async Task InitAsync(MovieContext context)
    {
        if (await context.Movies.AnyAsync()) return;

        var actors = GenerateActors(50);
        await context.AddRangeAsync(actors);

        var movies = GenerateMovies(50, actors);
        await context.AddRangeAsync(movies);

        await context.SaveChangesAsync();
    }

    private static List<Movie> GenerateMovies(int numberOfMovies, List<Actor> actors)
    {
        var movies = new List<Movie>();
        Random rand = new Random();
        var genreList = new List<string> { "Action", "Romance", "Drama", "Thriller", "Horror",
            "Comedy", "Western", "Fantasy", "Science Fiction", "Documentary",
            "Musical", "Crime", "Animation", "Sport", "Historical"};
        var languageList = new List<string> { "Swedish", "English", "Spanish", "French", "German", "Italian" };
        var roleList = new List<string> { "Main Antagonist", "Main Protagonist", "Lead", "Supporting", "Background", "Extra", "Bit" };

        for (int i = 0; i < numberOfMovies; i++)
        {
            var titleSize = rand.Next(2, 8);
            var titleWords = faker.Lorem.Words(titleSize);
            var titleJoin = string.Join(" ", titleWords);
            var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(titleJoin);

            var year = rand.Next(1900, 2025);
            var genre = genreList[rand.Next(0, genreList.Count)];
            //Duration in minutes
            var duration = rand.Next(45, 300);
            var budget = rand.Next(50000, 500000000);

            int numActors = faker.Random.Int(3, actors.Count / 2);
            var selectActors = faker.PickRandom(actors, numActors).ToList();

            var movieActors = faker.PickRandom(actors, numActors).ToList();

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
                MovieActors = selectActors.Select(actor => new MovieActor
                {
                    Actor = actor,
                    Role = roleList[rand.Next(roleList.Count)]
                }).ToList(),

                Reviews = GenerateReviews(rand.Next(1, 10)),
                //MovieActors = movieActors,

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
            var birthYear = rand.Next(1900, 2015);
            var actor = new Actor { Name = name, BirthYear = birthYear };
            actors.Add(actor);

        }
        return actors;
    }
    private static List<Review> GenerateReviews(int numberOfReviews)
    {
        var reviews = new List<Review>();
        Random rand = new Random();

        for (int i = 0; i < numberOfReviews; i++)
        {
            var reviewerName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Name.FullName());
            var comment = faker.Rant.Review("Movie"); //.Random.String(faker.Random.Int(1,25));
            var rating = rand.Next(1, 6);
            var review = new Review { ReviewerName = reviewerName, Comment = comment!, Rating = rating };
            reviews.Add(review);
        }


        return reviews;
    }
}
