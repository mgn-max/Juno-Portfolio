using Juno.Domain.Entities;
using Juno.Domain.Interfaces;
using Juno.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Juno.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JunoDbContext _context;

        public UserRepository(JunoDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailExceptId(string email, Guid id)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email && u.Id != id);
        }

        public async Task<bool> ExistsByLogin(string login)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Login == login);
        }

        public Task<bool> ExistsByLoginExceptId(string login, Guid id)
        {
            return _context.Users.AsNoTracking().AnyAsync(u => u.Login == login && u.Id != id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<User?> GetByLoginOrEmail(string loginOrEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
