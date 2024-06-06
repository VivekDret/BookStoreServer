using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreServer.Repository
{
    public class CartDetailRepository : IRepository<CartDetail>
    {
        private readonly BookStoreContext _context;
        public CartDetailRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<CartDetail> CreateAsync(CartDetail dbRecord)
        {
            _context.CartDetails.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(CartDetail dbRecord)
        {
            _context.CartDetails.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CartDetail>> GetAllAsync()
        {
            return await _context.CartDetails
            .Include(u => u.Cart)
            .Include(u => u.Book)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<CartDetail> GetAsync(Expression<Func<CartDetail, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.CartDetails.AsNoTracking().Where(filter)
                .Include(u => u.Cart)
                .Include(u => u.Book)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.CartDetails.Where(filter)
                .Include(u => u.Cart)
                .Include(u => u.Book)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<CartDetail> GetAsyncByName(Expression<Func<CartDetail, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.CartDetails.AsNoTracking().Where(filter)
                .Include(u => u.Cart)
                .Include(u => u.Book)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.CartDetails.Where(filter)
                .Include(u => u.Cart)
                .Include(u => u.Book)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<CartDetail> UpdateAsync(CartDetail dbRecord)
        {
            _context.CartDetails.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
