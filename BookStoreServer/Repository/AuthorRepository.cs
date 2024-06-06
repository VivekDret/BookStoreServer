using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace BookStoreServer.Repository
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly BookStoreContext _context;
        public AuthorRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<Author> CreateAsync(Author dbRecord)
        {
            _context.Authors.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Author dbRecord)
        {
            _context.Authors.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
            .Include(u => u.Books)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Author> GetAsync(Expression<Func<Author, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Authors.AsNoTracking().Where(filter)
                .Include(u => u.Books)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Authors.Where(filter)
                .Include(u => u.Books)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Author> GetAsyncByName(Expression<Func<Author, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Authors.AsNoTracking().Where(filter)
                .Include(u => u.Books)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Authors.Where(filter)
                .Include(u => u.Books)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Author> UpdateAsync(Author dbRecord)
        {
            _context.Authors.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
