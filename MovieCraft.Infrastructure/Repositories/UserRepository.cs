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
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user); 
        await _dbContext.SaveChangesAsync(); 
    }
}
