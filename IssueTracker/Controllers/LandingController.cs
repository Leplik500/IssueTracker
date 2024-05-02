using IssueTracker.Domain.ViewModels.User;
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

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        return Ok();
    }
}