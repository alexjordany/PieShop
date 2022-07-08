using Microsoft.EntityFrameworkCore;
using PieShop.Contracts.Persistence;
using PieShop.Models;

namespace PieShop.Persistence.Repositories;

public class ShoppingCart : IShoppingCart
{
    private readonly PieShopDbContext _dbContext;

    public string? ShoppingCartId { get; set; }

    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

    private ShoppingCart(PieShopDbContext bethanysPieShopDbContext)
    {
        _dbContext = bethanysPieShopDbContext;
    }

    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        PieShopDbContext context = services.GetService<PieShopDbContext>() ?? throw new Exception("Error initializing");

        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session?.SetString("CartId", cartId);

        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }

    public void AddToCart(Pie pie)
    {
        var shoppingCartItem =
                _dbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Pie = pie,
                Amount = 1
            };

            _dbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }
        _dbContext.SaveChanges();
    }

    public int RemoveFromCart(Pie pie)
    {
        var shoppingCartItem =
                _dbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

        var localAmount = 0;

        if (shoppingCartItem is not null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
                localAmount = shoppingCartItem.Amount;
            }
            else
            {
                _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        _dbContext.SaveChanges();

        return localAmount;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??=
                   _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(s => s.Pie)
                       .ToList();
    }

    public void ClearCart()
    {
        var cartItems = _dbContext
            .ShoppingCartItems
            .Where(cart => cart.ShoppingCartId == ShoppingCartId);

        _dbContext.ShoppingCartItems.RemoveRange(cartItems);

        _dbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        var total = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
            .Select(c => c.Pie.Price * c.Amount).Sum();
        return total;
    }
}
