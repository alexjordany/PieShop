using PieShop.Contracts.Persistence;

namespace PieShop.ViewModels;

public class ShoppingCartViewModel
{
    public IShoppingCart ShoppingCart { get; }
    public decimal ShoppingCartTotal { get; }

    public ShoppingCartViewModel(IShoppingCart shoppingCart, decimal shoppingCartTotal)
    {
        ShoppingCart = shoppingCart;
        ShoppingCartTotal = shoppingCartTotal;
    }
}
