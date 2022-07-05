using PieShop.Models;

namespace PieShop.Contracts.Persistence;

public interface ICategoryRepository
{
    IEnumerable<Category> AllCategories { get; }
}
