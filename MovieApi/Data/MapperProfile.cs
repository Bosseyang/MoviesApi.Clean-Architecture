using AutoMapper;
using MovieApi.Models.DTOs;
using MovieApi.Models.Entities;

namespace MovieApi.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //Movie
        CreateMap<Movie, MovieDto>();
            //Dont show Actors[] as empty if we choose to not show actors
            //.ForMember(dest => dest.Actors, opt => opt.Condition(src => src.Actors.Any()));
        CreateMap<Movie, MovieDetailDto>()
            .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.MovieDetails.Synopsis))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MovieDetails.Language))
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.MovieDetails.Budget));
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
        CreateMap<Actor, ActorDto>();
        CreateMap<ActorDto, Actor>();

        // Review
        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewDto, Review>();
        //CreateMap<Movie, ReviewDto>();
    }
}
