using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUserIdAsync(string userId);
    Task UpdateAsync(User user);
}
