using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CartStoreServer.Repository
{
    public class CartRepository : IRepository<Cart> 
    {
        private readonly BookStoreContext _context;
        public CartRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateAsync(Cart dbRecord)
        {
            _context.Carts.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Cart dbRecord)
        {
            _context.Carts.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _context.Carts
            .Include(u => u.User)
            .Include(u => u.CartDetails)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Cart> GetAsync(Expression<Func<Cart, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                 return await _context.Carts.AsNoTracking().Where(filter)
                .Include(u => u.User)
                .Include(u => u.CartDetails)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Carts.Where(filter)
                .Include(u => u.User)
                .Include(u => u.CartDetails)
                .AsSplitQuery()   
                .FirstOrDefaultAsync();
        }

        public async Task<Cart> GetAsyncByName(Expression<Func<Cart, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Carts.AsNoTracking().Where(filter)
                .Include(u => u.User)
                .Include(u => u.CartDetails)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Carts.Where(filter)
                .Include(u => u.User)
                .Include(u => u.CartDetails)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<Cart> UpdateAsync(Cart dbRecord)
        {
            _context.Carts.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
