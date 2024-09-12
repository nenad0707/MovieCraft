using MediatR;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Features.Users.Commands
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public SaveUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByUserIdAsync(request.UserId);

            if (existingUser == null)
            {
              var newUser = new User
                {
                    UserId = request.UserId,
                    Name = request.Name,
                  FavoriteMovies = new List<FavoriteMovie>()
              };

              await _userRepository.AddAsync(existingUser!);
            }

            return Unit.Value;
        }
    }
}
