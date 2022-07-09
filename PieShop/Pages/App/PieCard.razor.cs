using Microsoft.AspNetCore.Components;
using PieShop.Models;

namespace PieShop.Pages.App;

public partial class PieCard
{
    [Parameter]
    public Pie? Pie { get; set; }
}
