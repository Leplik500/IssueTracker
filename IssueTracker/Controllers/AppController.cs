using IssueTracker.Domain.ViewModels.Issue;
using IssueTracker.Hubs;
using IssueTracker.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace IssueTracker.Controllers;

public class AppController : Controller {
    private readonly IIssueService _issueService;
    private readonly IUserService _userService;
    private readonly IHubContext<CommentsHub> _hubContext;

    public AppController(IIssueService issueService, IUserService userService, IHubContext<CommentsHub> hubContext)
    {
        _issueService = issueService;
        _userService = userService;
        _hubContext = hubContext;
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

    [HttpGet]
    public async Task<IActionResult> IssueDetails(Int64 id)
    {
        var response = await _issueService.GetIssue(id);
        return PartialView(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(String message, Int64 issueId)
    {
        var response = await _issueService.AddComment(message, issueId);
        if (response.StatusCode == Domain.Enum.StatusCode.OK){
            await _hubContext.Clients
                .All
                .SendAsync("ReceiveComment", message, issueId);
            return Ok(new {description = response.Description});
        }

        return BadRequest(new {description = response.Description});

    }

    public IActionResult Issues()
    {
        return View();
    }
}