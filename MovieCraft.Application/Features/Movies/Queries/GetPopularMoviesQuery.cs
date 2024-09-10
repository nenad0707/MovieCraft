using MediatR;

namespace MovieCraft.Application.Features.Movies.Queries;

public class GetPopularMoviesQuery : IRequest<IEnumerable<MovieDto>>
{
}
