using Microsoft.AspNetCore.Mvc;
using PieShop.Contracts.Persistence;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers;
public class PiesController : Controller
{
    private readonly IPieRepository _pieRepository;
    private readonly ICategoryRepository _categoryRepository;

    public PiesController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
    {
        _pieRepository = pieRepository;
        _categoryRepository = categoryRepository;
    }

    public ViewResult List(string category)
    {
        IEnumerable<Pie> pies;
        string? currentCategory;

        if (string.IsNullOrEmpty(category))
        {
            pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
            currentCategory = "All pies";
        }
        else
        {
            pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category).OrderBy(p => p.PieId);
            currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
        }

        return View(new PieListViewModel(pies, currentCategory));
    }

    public IActionResult Details(int id)
    {
        var pie = _pieRepository.GetPieById(id);
        if(pie is not null)
            return View(pie);

        return NotFound();
    }
}
