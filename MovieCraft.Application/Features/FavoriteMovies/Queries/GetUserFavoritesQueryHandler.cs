using AutoMapper;

namespace MovieCraft.Application.Features.FavoriteMovies.Queries;

public class GetUserFavoritesQueryHandler : IRequestHandler<GetUserFavoritesQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserFavoritesQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserFavoritesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserIdAsync(request.UserId);
        return _mapper.Map<UserDto>(user);
    }
}
