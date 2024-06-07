using IssueTracker.DAL.Interfaces;
using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Enum;
using IssueTracker.Domain.Extensions;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.Issue;
using IssueTracker.Domain.ViewModels.User;
using IssueTracker.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IssueTracker.Service.Implementations;

public class IssueService : IIssueService {
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
        try{
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

            var issueEntity = new IssueEntity();

            issueEntity.Description = model.Description;

            var assigneeEmails = model.Assignees.Select(assignee => assignee.Split(',')[1].Trim()).ToList();
            issueEntity.Assignees = _userRepository.GetAll()
                .Where(user => assigneeEmails.Any(email => user.Email.Equals(email)))
                .ToList();

            issueEntity.Comments = new List<String>();
            issueEntity.Title = model.Title;
            issueEntity.Tags = model.Tags[0].Split(" ").ToList();
            issueEntity.Status = IssueStatus.New;
            issueEntity.Priority = model.Priority;
            issueEntity.Created = DateTime.UtcNow;
            issueEntity.Id = Guid.NewGuid();

            await _issueRepository.Create(issueEntity);
            return new BaseResponse<IssueEntity>()
            {
                Description = $"User created - {issueEntity.Title} - {issueEntity.Created}",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e){
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
        try{
            var issues = await _issueRepository
                .GetAll()
                .Select(
                x => new IssueViewModel
                {
                    Description = x.Description,
                    Assignees = x.Assignees
                        .Select(userEntity => new UserViewModel
                        {
                            Email = userEntity.Email,
                            FirstName = userEntity.FirstName,
                            LastName = userEntity.LastName,
                            Role = userEntity.Role.GetDisplayName()
                        })
                        .ToList(),
                    Comments = x.Comments,
                    Title = x.Title,
                    Tags = x.Tags,
                    Status = x.Status.GetDisplayName(),
                    Priority = x.Priority.GetDisplayName(),
                    Created = x.Created.ToLongDateString()
                })
                .ToListAsync();

            return
                new BaseResponse<IEnumerable<IssueViewModel>>()
                {
                    Data = issues,
                    StatusCode = StatusCode.OK
                };
        }
        catch (Exception e){
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