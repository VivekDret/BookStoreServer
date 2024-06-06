using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreServer.Repository
{
    public class RefundRepository : IRepository<Refund>
    {
        private readonly BookStoreContext _context;
        public RefundRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<Refund> CreateAsync(Refund dbRecord)
        {
            _context.Refunds.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Refund dbRecord)
        {
            _context.Refunds.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Refund>> GetAllAsync()
        {
            return await _context.Refunds
             .Include(u => u.Payment)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Refund> GetAsync(Expression<Func<Refund, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Refunds.AsNoTracking().Where(filter)
                .Include(u => u.Payment)
               .AsSplitQuery()
               .FirstOrDefaultAsync();
            else
                return await _context.Refunds.Where(filter)
                .Include(u => u.Payment)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Refund> GetAsyncByName(Expression<Func<Refund, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Refunds.AsNoTracking().Where(filter)
                .Include(u => u.Payment)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Refunds.Where(filter)
                .Include(u => u.Payment)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Refund> UpdateAsync(Refund dbRecord)
        {
            _context.Refunds.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
