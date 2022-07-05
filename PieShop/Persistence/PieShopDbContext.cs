using Microsoft.EntityFrameworkCore;
using PieShop.Models;

namespace PieShop.Persistence;

public class PieShopDbContext: DbContext
{
    public PieShopDbContext(DbContextOptions<PieShopDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Pie> Pies { get; set; }

}
