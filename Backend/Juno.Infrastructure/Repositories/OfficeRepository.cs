using Juno.Domain.Entities;
using Juno.Domain.Interfaces;
using Juno.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Juno.Infrastructure.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly JunoDbContext _context;
        public OfficeRepository(JunoDbContext context)
        {
            _context = context;
        }
        
        public async Task Add(Office office)
        {
            await _context.Offices.AddAsync(office);
        }

        public async Task<bool> ExistsByDocumentNumber(string documentNumber)
        {
            return await _context.Offices.AsNoTracking().AnyAsync(o => o.DocumentNumber == documentNumber);
        }

        public async Task<bool> ExistsByDocumentNumberExceptId(string documentNumber, Guid id)
        {
            return await _context.Offices.AsNoTracking().AnyAsync(o => o.DocumentNumber == documentNumber && o.Id != id);
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _context.Offices.AsNoTracking().AnyAsync(o => o.Email == email);
        }

        public async Task<bool> ExistsByEmailExceptId(string email, Guid id)
        {
            return await _context.Offices.AsNoTracking().AnyAsync(o => o.Email == email && o.Id != id);
        }

        public async Task<Office?> GetById(Guid id)
        {
            return await _context.Offices.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
