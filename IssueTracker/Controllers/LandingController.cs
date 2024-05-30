using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.User;
using IssueTracker.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers;

public class LandingController : Controller
{
    private readonly IUserService _userService;

    public LandingController(IUserService userService)
    {
        _userService = userService;
    }

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
        var response = await _userService.Create(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
            return Ok(new {description = response.Description});

        return BadRequest(new {description = response.Description});
    }

    [HttpGet]
    public async Task<IActionResult> Authenthicate(CreateUserViewModel model)
    {
        var response = await _userService.Authenticate(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
            return Ok(new {description = response.Description});

        return BadRequest(new {description = response.Description});
    }
}