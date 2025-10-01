using Navora.OrderService.Domain.Enums;

namespace Navora.OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string PickupAddress { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PickedUpAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public Guid? DriverId { get; set; }

    // Constructor
    public Order()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    // Business logic methods (not just data!)
    public void AssignDriver(Guid driverId)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Can only assign driver to pending orders");

        DriverId = driverId;
        Status = OrderStatus.Assigned;
    }

    public void MarkAsPickedUp()
    {
        if (Status != OrderStatus.Assigned)
            throw new InvalidOperationException("Order must be assigned before pickup");

        Status = OrderStatus.InTransit;
        PickedUpAt = DateTime.UtcNow;
    }

    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.InTransit)
            throw new InvalidOperationException("Order must be in transit before delivery");

        Status = OrderStatus.Delivered;
        DeliveredAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Cannot cancel a delivered order");

        Status = OrderStatus.Cancelled;
    }
}