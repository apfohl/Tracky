using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tracky.Presentation.Models;

namespace Tracky.Presentation.Controllers;

public sealed class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> logger = logger;

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
