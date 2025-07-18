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
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src =>
                src.MovieActors.Select(ma => ma.Actor)));

        //Dont show Actors[] as empty if we choose to not show actors
        //.ForMember(dest => dest.Actors, opt => opt.Condition(src => src.Actors.Any()));
        CreateMap<Movie, MovieDetailDto>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.MovieDetails.Synopsis))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MovieDetails.Language))
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.MovieDetails.Budget))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src =>
                src.MovieActors.Select(ma => ma.Actor)));

        CreateMap<MovieCreateDto, Movie>()
            .ForMember(dest => dest.Genre, opt => opt.Ignore())
            .ForMember(dest => dest.GenreId, opt => opt.Ignore());
        //.ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));
        CreateMap<MovieUpdateDto, Movie>()
            .ForMember(dest => dest.Genre, opt => opt.Ignore())
            .ForMember(dest => dest.GenreId, opt => opt.Ignore())
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
        CreateMap<ReviewCreateDto, Review>();
        //CreateMap<Movie, ReviewDto>();

        //MovieActor
        CreateMap<MovieActorCreateDto, MovieActor>();
        CreateMap<MovieActor, ActorDto>();
        CreateMap<MovieActor, MovieActorDto>()
            .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.Actor.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Actor.Name))
            .ForMember(dest => dest.BirthYear, opt => opt.MapFrom(src => src.Actor.BirthYear))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}
