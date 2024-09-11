using Microsoft.EntityFrameworkCore;
using MovieCraft.Application.Interfaces;
using MovieCraft.Domain.Entities;
using MovieCraft.Infrastructure.Persistence;

namespace MovieCraft.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MovieDbContext _dbContext;

    public UserRepository(MovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByUserIdAsync(string userId)
    {
        return await _dbContext.Users
            .Include(u => u.FavoriteMovies)
            .ThenInclude(fm => fm.Movie)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }


    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
