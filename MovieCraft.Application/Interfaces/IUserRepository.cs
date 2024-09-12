using MovieCraft.Domain.Entities;

namespace MovieCraft.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUserIdAsync(string userId);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
}
