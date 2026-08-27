using Juno.Domain.Entities;
using Juno.Domain.Enums.OfficeMembershipEnums;
using Juno.Domain.Interfaces;
using Juno.Domain.Projections.OfficeMembershipProjection;
using Juno.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Juno.Infrastructure.Repositories
{
    public class OfficeMembershipRepository : IOfficeMembershipRepository
    {
        private readonly JunoDbContext _context;

        public OfficeMembershipRepository(JunoDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(OfficeMembership membership)
        {
            await _context.OfficeMemberships.AddAsync(membership);
        }

        public async Task<int> CountPartnersByOfficeId(Guid officeId)
        {
            return await _context.OfficeMemberships.AsNoTracking().CountAsync(o => o.OfficeId == officeId && o.Profile == MembershipProfile.Partner);
        }

        public async Task<bool> ExistsByUserAndOffice(Guid userId, Guid officeId)
        {
            return await _context.OfficeMemberships.AsNoTracking().AnyAsync(m => m.UserId == userId && m.OfficeId == officeId);
        }

        public async Task<OfficeMembership?> GetById(Guid id)
        {
            return await _context.OfficeMemberships.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<OfficeMembership>> GetByOfficeId(Guid officeId)
        {
            return await _context.OfficeMemberships.AsNoTracking().Where(m => m.OfficeId == officeId).OrderByDescending(m => m.CreatedAt).ToListAsync();
        }

        public async Task<List<OfficeMembershipWithDetails>> GetByOfficeIdWithDetails(Guid officeId)
        {
            return await (
                from membership in _context.OfficeMemberships.AsNoTracking()
                join user in _context.Users.AsNoTracking()
                    on membership.UserId equals user.Id
                join office in _context.Offices.AsNoTracking()
                    on membership.OfficeId equals office.Id
                where membership.OfficeId == officeId
                orderby membership.CreatedAt descending
                select new OfficeMembershipWithDetails
                (
                    membership.Id,
                    membership.UserId,
                    user.Name,
                    membership.OfficeId,
                    office.Name,
                    membership.Profile,
                    membership.Status,
                    membership.CreatedAt
                )
            ).ToListAsync();
        }

        public async Task<OfficeMembership?> GetByUserAndOffice(Guid userId, Guid officeId)
        {
            return await _context.OfficeMemberships.FirstOrDefaultAsync(m => m.UserId == userId && m.OfficeId == officeId);
        }

        public async Task<List<OfficeMembership>> GetByUserId(Guid userId)
        {
            return await _context.OfficeMemberships.AsNoTracking().Where(m => m.UserId == userId).OrderByDescending(m => m.CreatedAt).ToListAsync();  
        }

        public async Task<List<OfficeMembershipWithDetails>> GetByUserIdWithDetails(Guid userId)
        {
            return await (
                from membership in _context.OfficeMemberships.AsNoTracking()
                join user in _context.Users.AsNoTracking()
                    on membership.UserId equals user.Id
                join office in _context.Offices.AsNoTracking()
                    on membership.OfficeId equals office.Id
                where membership.UserId == userId
                orderby membership.CreatedAt descending
                select new OfficeMembershipWithDetails
                (
                    membership.Id,
                    membership.UserId,
                    user.Name,
                    membership.OfficeId,
                    office.Name,
                    membership.Profile,
                    membership.Status,
                    membership.CreatedAt
                )
            ).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
