using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace BookStoreServer.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly BookStoreContext _context;
        public CategoryRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category dbRecord)
        {
            _context.Categories.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Category dbRecord)
        {
            _context.Categories.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
            .Include(u => u.Books)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Category> GetAsync(Expression<Func<Category, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Categories.AsNoTracking().Where(filter)
                .Include(u => u.Books)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Categories.Where(filter)
                .Include(u => u.Books)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Category> GetAsyncByName(Expression<Func<Category, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Categories.AsNoTracking().Where(filter)
                .Include(u => u.Books)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Categories.Where(filter)
                .Include(u => u.Books)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Category> UpdateAsync(Category dbRecord)
        {
            _context.Categories.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
