using PieShop.Models;

namespace PieShop.Contracts.Persistence;

public interface IOrderRepository
{
    void CreateOrder(Order order);
}
