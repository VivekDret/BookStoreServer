using BookStoreServer.Context;
using BookStoreServer.Interface;
using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStoreServer.Repository
{
    public class OrderDetailRepository : IRepository<OrderDetail> 
    {
        private readonly BookStoreContext _context;
        public OrderDetailRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<OrderDetail> CreateAsync(OrderDetail dbRecord)
        {
            _context.OrderDetails.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(OrderDetail dbRecord)
        {
            _context.OrderDetails.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _context.OrderDetails
            .Include(u => u.Order)
            .Include(u => u.Book)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<OrderDetail> GetAsync(Expression<Func<OrderDetail, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.OrderDetails.AsNoTracking().Where(filter)
                .Include(u => u.Order)
                .Include(u => u.Book)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.OrderDetails.Where(filter)
                .Include(u => u.Order)
                .Include(u => u.Book)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDetail> GetAsyncByName(Expression<Func<OrderDetail, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.OrderDetails.AsNoTracking().Where(filter)
                .Include(u => u.Order)
                .Include(u => u.Book)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.OrderDetails.Where(filter)
                .Include(u => u.Order)
                .Include(u => u.Book)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDetail> UpdateAsync(OrderDetail dbRecord)
        {
            _context.OrderDetails.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
