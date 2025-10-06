using Microsoft.EntityFrameworkCore;
using Navora.OrderService.Domain.Entities;
using Navora.OrderService.Domain.Enums;
using Navora.OrderService.Domain.Interfaces;
using Navora.OrderService.Infrastructure.Data;

namespace Navora.OrderService.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<Order?> GetByOrderNumberAsync(string orderNumber)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status)
    {
        return await _context.Orders
            .Where(o => o.Status == status)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await GetByIdAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Orders.AnyAsync(o => o.Id == id);
    }
}