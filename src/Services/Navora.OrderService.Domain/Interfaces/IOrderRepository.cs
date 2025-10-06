using Navora.OrderService.Domain.Entities;
using Navora.OrderService.Domain.Enums;

namespace Navora.OrderService.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<Order?> GetByOrderNumberAsync(string orderNumber);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}