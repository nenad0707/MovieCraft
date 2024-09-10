using MediatR;
using MovieCraft.Application.DTOs;

namespace MovieCraft.Application.Features.Movies.Queries;

public class GetPopularMoviesQuery : IRequest<IEnumerable<MovieDto>>
{
}
