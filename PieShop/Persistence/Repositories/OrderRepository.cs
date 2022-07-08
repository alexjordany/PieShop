using PieShop.Contracts.Persistence;
using PieShop.Models;

namespace PieShop.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly PieShopDbContext _dbContext;
    private readonly IShoppingCart _shoppingCart;

    public OrderRepository(PieShopDbContext dbContext, IShoppingCart shoppingCart)
    {
        _dbContext = dbContext;
        _shoppingCart = shoppingCart;
    }

    public void CreateOrder(Order order)
    {
        order.OrderPlaced = DateTime.Now;

        List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
        order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

        order.OrderDetails = new List<OrderDetail>();

        foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
        {
            var orderDetail = new OrderDetail
            {
                Amount = shoppingCartItem.Amount,
                PieId = shoppingCartItem.Pie.PieId,
                Price = shoppingCartItem.Pie.Price
            };
            order.OrderDetails.Add(orderDetail);
        }

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }
}
