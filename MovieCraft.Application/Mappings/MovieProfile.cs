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
          .ForMember(dest => dest.Id, opt => opt.Ignore()) 
          .ForMember(dest => dest.BackdropPath, opt => opt.MapFrom(src => src.BackdropPath));

        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TmdbId))
            .ForMember(dest => dest.BackdropPath, opt => opt.MapFrom(src => src.BackdropPath));

        CreateMap<AddMovieCommand, Movie>()
            .ForMember(dest => dest.TmdbId, opt => opt.MapFrom(src => src.TmdbId))
            .ForMember(dest => dest.BackdropPath, opt => opt.MapFrom(src => src.BackdropPath));
    }
}
