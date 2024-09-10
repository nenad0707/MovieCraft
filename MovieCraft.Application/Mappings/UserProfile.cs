using AutoMapper;
using MovieCraft.Application.DTOs;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<FavoriteMovie, FavoriteMovieDto>()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Movie.TmdbId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
            .ForMember(dest => dest.Overview, opt => opt.MapFrom(src => src.Movie.Overview))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Movie.ReleaseDate))
            .ForMember(dest => dest.PosterPath, opt => opt.MapFrom(src => src.Movie.PosterPath));
    }
}
