using Microsoft.AspNetCore.Mvc;
using PieShop.Contracts.Persistence;
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

    public IActionResult List()
    {
        PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "Cheese cakes");
        return View(pieListViewModel);
    }
}
