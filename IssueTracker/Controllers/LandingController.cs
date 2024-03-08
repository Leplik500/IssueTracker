using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers;

public class LandingController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Pricing()
    {
        return View();
    }
}