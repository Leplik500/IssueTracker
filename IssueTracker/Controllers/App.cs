using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers;

public class App : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}