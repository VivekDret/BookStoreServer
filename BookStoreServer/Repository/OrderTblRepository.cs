using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreServer.Repository
{
    public class OrderTblRepository : IRepository<OrderTbl>
    {
        private readonly BookStoreContext _context;
        public OrderTblRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<OrderTbl> CreateAsync(OrderTbl dbRecord)
        {
            _context.Orders.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(OrderTbl dbRecord)
        {
            _context.Orders.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderTbl>> GetAllAsync()
        {
            return await _context.Orders
             .Include(u => u.Payment)
             .Include(u => u.User)
             .Include(u => u.OrderDetails)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<OrderTbl> GetAsync(Expression<Func<OrderTbl, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Orders.AsNoTracking().Where(filter)
                .Include(u => u.Payment)
                .Include(u => u.User)
                .Include(u => u.OrderDetails)
               .AsSplitQuery()
               .FirstOrDefaultAsync();
            else
                return await _context.Orders.Where(filter)
                .Include(u => u.Payment)
                 .Include(u => u.User)
                 .Include(u => u.OrderDetails)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<OrderTbl> GetAsyncByName(Expression<Func<OrderTbl, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Orders.AsNoTracking().Where(filter)
                .Include(u => u.Payment)
                 .Include(u => u.User)
                 .Include(u => u.OrderDetails)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Orders.Where(filter)
                .Include(u => u.Payment)
                 .Include(u => u.User)
                 .Include(u => u.OrderDetails)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<OrderTbl> UpdateAsync(OrderTbl dbRecord)
        {
            _context.Orders.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
