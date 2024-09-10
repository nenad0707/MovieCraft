using AutoMapper;
using MovieCraft.Application.DTOs;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Mappings;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>();

        CreateMap<MovieDto, Movie>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
