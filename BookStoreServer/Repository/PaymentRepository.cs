using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace BookStoreServer.Repository
{
    public class PaymentRepository : IRepository<Payment>
    {
        private readonly BookStoreContext _context;
        public PaymentRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreateAsync(Payment dbRecord)
        {
            _context.Payments.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Payment dbRecord)
        {
            _context.Payments.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments
             .Include(u => u.Refund)
             .Include(u => u.Order)
             .Include(u => u.User)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Payments.AsNoTracking().Where(filter)
                .Include(u => u.Refund)
                 .Include(u => u.Order)
                 .Include(u => u.User)
               .AsSplitQuery()
               .FirstOrDefaultAsync();
            else
                return await _context.Payments.Where(filter)
                .Include(u => u.Refund)
                 .Include(u => u.Order)
                 .Include(u => u.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Payment> GetAsyncByName(Expression<Func<Payment, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Payments.AsNoTracking().Where(filter)
                .Include(u => u.Refund)
                 .Include(u => u.Order)
                 .Include(u => u.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Payments.Where(filter)
                .Include(u => u.Refund)
                 .Include(u => u.Order)
                 .Include(u => u.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Payment> UpdateAsync(Payment dbRecord)
        {
            _context.Payments.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
