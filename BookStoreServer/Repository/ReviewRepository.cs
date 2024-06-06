using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreServer.Repository
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly BookStoreContext _context;
        public ReviewRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<Review> CreateAsync(Review dbRecord)
        {
            _context.Reviews.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Review dbRecord)
        {
            _context.Reviews.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews
             .Include(u => u.Book)
             .Include(u => u.User)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Review> GetAsync(Expression<Func<Review, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Reviews.AsNoTracking().Where(filter)
                .Include(u => u.Book)
                .Include(u => u.User)
               .AsSplitQuery()
               .FirstOrDefaultAsync();
            else
                return await _context.Reviews.Where(filter)
                .Include(u => u.Book)
                 .Include(u => u.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Review> GetAsyncByName(Expression<Func<Review, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Reviews.AsNoTracking().Where(filter)
                .Include(u => u.Book)
             .Include(u => u.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Reviews.Where(filter)
                .Include(u => u.Book)
                 .Include(u => u.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Review> UpdateAsync(Review dbRecord)
        {
            _context.Reviews.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
