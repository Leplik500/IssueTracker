using IssueTracker.Domain.ViewModels.Issue;
using IssueTracker.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers;

public class AppController : Controller
{
    private readonly IIssueService _issueService;
    private readonly IUserService _userService;

    public AppController(IIssueService issueService, IUserService userService)
    {
        _issueService = issueService;
        _userService = userService;
    }

    public IActionResult CreateIssue()
    {
        var model = new CreateIssueViewModel()
        {
            AvaliableUsers = _userService
                .GetAll()
                .Result
                .Data
                .Select(x => $"{x.Role} {x.FirstName} {x.LastName}, {x.Email}")
                .ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateIssueViewModel model)
    {
        var response = await _issueService.Create(model);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
            return Ok(new {description = response.Description});

        return BadRequest(new {description = response.Description});
    }

    [HttpPost]
    public async Task<IActionResult> GetIssues()
    {
        var response = await _issueService.GetAll();
        return Json(new {data = response.Data});
    }

    public IActionResult Issues()
    {
        return View();
    }
}