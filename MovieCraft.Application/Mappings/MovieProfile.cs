using AutoMapper;
using MovieCraft.Application.DTOs;
using MovieCraft.Application.Features.Movies.Commands;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Mappings;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<MovieDto, Movie>()
                .ForMember(dest => dest.TmdbId, opt => opt.MapFrom(src => src.Id)) 
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Movie, MovieDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TmdbId));

        CreateMap<AddMovieCommand, Movie>();
    }
}
