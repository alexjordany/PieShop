using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PieShop.Contracts.Persistence;
using PieShop.Models;
using PieShop.Persistence;
using PieShop.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PieShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:PieShopDbContextConnection"]);
});


var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.MapDefaultControllerRoute();
app.MapRazorPages();
DbInitializer.Seed(app);
app.Run();
