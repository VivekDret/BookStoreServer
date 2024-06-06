using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace BookStoreServer.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookStoreContext _context;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(Book dbRecord)
        {
            try
            {
                _context.Books.Add(dbRecord);
                await _context.SaveChangesAsync();
                return dbRecord;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
        }

        public async Task<bool> DeleteAsync(Book dbRecord)
        {
            _context.Books.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
            .Include(u => u.Author)
            .Include(u => u.Category)
            .Include(u => u.CartDetails)
            .Include(u => u.OrdertDetails)
            .Include(u => u.Reviews)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Book> GetAsync(Expression<Func<Book, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Books.AsNoTracking().Where(filter)
                .Include(u => u.Author)
                .Include(u => u.Category)
                .Include(u => u.CartDetails)
                .Include(u => u.OrdertDetails)
                .Include(u => u.Reviews)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Books.Where(filter)
                .Include(u => u.Author)
                .Include(u => u.Category)
                .Include(u => u.CartDetails)
                .Include(u => u.OrdertDetails)
                .Include(u => u.Reviews)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Book> GetAsyncByName(Expression<Func<Book, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Books.AsNoTracking().Where(filter)
                .Include(u => u.Author)
                .Include(u => u.Category)
                .Include(u => u.CartDetails)
                .Include(u => u.OrdertDetails)
                .Include(u => u.Reviews)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Books.Where(filter)
                .Include(u => u.Author)
                .Include(u => u.Category)
                .Include(u => u.CartDetails)
                .Include(u => u.OrdertDetails)
                .Include(u => u.Reviews)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Book> UpdateAsync(Book dbRecord)
        {
            _context.Books.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
