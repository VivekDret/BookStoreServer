using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreServer.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly BookStoreContext _context;
        public UserRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User dbRecord)
        {
            _context.Users.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(User dbRecord)
        {
            _context.Users.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
            .Include(u => u.Carts)
            .Include(u => u.Reviews)
            .Include(u => u.Payments)
            .Include(u => u.Orders)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Users.AsNoTracking().Where(filter)
                .Include(u => u.Carts)
                .Include(u => u.Reviews)
                .Include(u => u.Payments)
                .Include(u => u.Orders)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Users.Where(filter)
                .Include(u => u.Carts)
                .Include(u => u.Reviews)
                .Include(u => u.Payments)
                .Include(u => u.Orders)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetAsyncByName(Expression<Func<User, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Users.AsNoTracking().Where(filter)
                .Include(u => u.Carts)
                .Include(u => u.Reviews)
                .Include(u => u.Payments)
                .Include(u => u.Orders)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Users.Where(filter)
                .Include(u => u.Carts)
                .Include(u => u.Reviews)
                .Include(u => u.Payments)
                .Include(u => u.Orders)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<User> UpdateAsync(User dbRecord)
        {
            _context.Users.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
