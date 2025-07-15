using AutoMapper;
using Movies.Core.DTOs;
using Movies.Core.Entities;

namespace Movies.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //Movie
        //CreateMap<Movie, MovieDto>();
        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src =>
                src.MovieActors.Select(ma => ma.Actor)));

        //Dont show Actors[] as empty if we choose to not show actors
        //.ForMember(dest => dest.Actors, opt => opt.Condition(src => src.Actors.Any()));
        CreateMap<Movie, MovieDetailDto>()
            .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.MovieDetails.Synopsis))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MovieDetails.Language))
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.MovieDetails.Budget))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src =>
                src.MovieActors.Select(ma => ma.Actor)));
        CreateMap<MovieCreateDto, Movie>();
        CreateMap<MovieUpdateDto, Movie>()
            .ForMember(dest => dest.MovieDetails, opt => opt.MapFrom((src, dest, _, context) =>
            {
                if (dest.MovieDetails == null)
                    dest.MovieDetails = new MovieDetails();

                dest.MovieDetails.Synopsis = src.Synopsis;
                dest.MovieDetails.Language = src.Language;
                dest.MovieDetails.Budget = src.Budget;
                return dest.MovieDetails;
            }));

        CreateMap<MovieDetails, MovieDetailCreateDto>().ReverseMap();
        //CreateMap<MovieDetails, MovieDetailDto>();

        // Actor
        CreateMap<Actor, ActorDto>().ReverseMap();

        // Review
        CreateMap<Review, ReviewDto>().ReverseMap();
        //CreateMap<Movie, ReviewDto>();

        //MovieActor
        CreateMap<MovieActorCreateDto, MovieActor>();
        CreateMap<MovieActor, ActorDto>();
    }
}
