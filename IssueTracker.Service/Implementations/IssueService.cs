using IssueTracker.DAL.Interfaces;
using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Enum;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.Issue;
using IssueTracker.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IssueTracker.Service.Implementations;

public class IssueService : IIssueService
{
    private readonly IBaseRepository<IssueEntity> _issueRepository;
    private readonly IBaseRepository<UserEntity> _userRepository;
    private ILogger<IssueService> _logger;

    public IssueService(IBaseRepository<IssueEntity> issueRepository, ILogger<IssueService> logger, IBaseRepository<UserEntity> userRepository)
    {
        _issueRepository = issueRepository;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<IBaseResponse<IssueEntity>> Create(CreateIssueViewModel model)
    {
        try
        {
            _logger.LogInformation($"Create issue: {model.Title}");
            var issue = await _issueRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Title == model.Title);

            if (issue != null)
                return new BaseResponse<IssueEntity>()
                {
                    Description = "Issue with this title already exists",
                    StatusCode = StatusCode.IssueIsHasAlready
                };

            var issueEntity = new IssueEntity()
            {
                Description = model.Description,
                Assignees = _userRepository
                    .GetAll()
                    .Where(x => model.AvaliableUsers.Contains(x.Email))
                    .ToList(),
                Comments = new List<string>(),
                Title = model.Title,
                Tags = model.Tags[0].Split(" ").ToList(),
                Status = IssueStatus.New,
                Created = DateTime.UtcNow,
                Id = Guid.NewGuid()
            };

            await _issueRepository.Create(issueEntity);
            return new BaseResponse<IssueEntity>()
            {
                Description = $"User created - {issueEntity.Title} - {issueEntity.Created}",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"[IssueService.Create]: {e.Message}");
            return new BaseResponse<IssueEntity>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = e.Message
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<IssueViewModel>>> GetAll()
    {
        try
        {
            var issues = await _issueRepository
                .GetAll()
                .Select(
                    x => new IssueViewModel
                    {
                        Description = x.Description,
                        Assignees = x.Assignees,
                        Comments = x.Comments,
                        Title = x.Title,
                        Tags = x.Tags,
                        Status = x.Status,
                        Priority = x.Priority,
                        Created = x.Created
                    })
                .ToListAsync();

            return
                new BaseResponse<IEnumerable<IssueViewModel>>()
                {
                    Data = issues,
                    StatusCode = StatusCode.OK
                };
        }
        catch (Exception e)
        {
            _logger.LogError($"[IssueService.GetAll]: {e.Message}");
            return
                new BaseResponse<IEnumerable<IssueViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = e.Message
                };
        }
    }
}