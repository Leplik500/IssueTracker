using IssueTracker.DAL.Interfaces;
using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Enum;
using IssueTracker.Domain.Extensions;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.Issue;
using IssueTracker.Domain.ViewModels.User;
using IssueTracker.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace IssueTracker.Service.Implementations;

public class IssueService : IIssueService {
    private readonly IBaseRepository<IssueEntity> _issueRepository;
    private readonly IBaseRepository<UserEntity> _userRepository;
    private readonly IRedisRepository _emojiRepository;
    private readonly ILogger<IssueService> _logger;

    public IssueService(IBaseRepository<IssueEntity> issueRepository, ILogger<IssueService> logger, IBaseRepository<UserEntity> userRepository, IRedisRepository emojiRepository)
    {
        _issueRepository = issueRepository;
        _logger = logger;
        _userRepository = userRepository;
        _emojiRepository = emojiRepository;
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

            var issueEntity = new IssueEntity
            {
                Description = model.Description
            };

            var assigneeEmails = model.Assignees.Select(assignee => assignee.Split(',')[1].Trim()).ToList();
            issueEntity.Assignees = _userRepository.GetAll()
                .Where(user => assigneeEmails.Any(email => user.Email.Equals(email)))
                .ToList();


            issueEntity.Comments = new List<String>();
            issueEntity.Title = model.Title;
            issueEntity.Tags = model.Tags[0].Split(" ").ToList();
            issueEntity.Status = issueEntity.Assignees.Count > 0 ? IssueStatus.Assigned : IssueStatus.New;
            issueEntity.Priority = model.Priority;
            issueEntity.Created = DateTime.UtcNow;

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
                    Id = x.Id,
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
    public async Task<IBaseResponse<IssueViewModel>> GetIssue(Int64 id)
    {
        try{
            var issue = await _issueRepository
                .GetAll()
                .Include(i => i.Assignees)
                .FirstOrDefaultAsync(x => id == x.Id);
            if (issue == null)
                return new BaseResponse<IssueViewModel>()
                {
                    Description = "Issue not found",
                    StatusCode = StatusCode.IssueNotFound
                };

            var issueModel = new IssueViewModel();
            issueModel.Id = issue.Id;
            issueModel.Description = issue.Description;
            issueModel.Assignees = issue.Assignees
                .Select(userEntity => new UserViewModel
                {
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Role = userEntity.Role.GetDisplayName()
                })
                .ToList();
            issueModel.Comments = await ReplaceShortcodesWithEmojis(issue.Comments);
            issueModel.Title = issue.Title;
            issueModel.Tags = issue.Tags;
            issueModel.Status = issue.Status.GetDisplayName();
            issueModel.Priority = issue.Priority.GetDisplayName();
            issueModel.Created = issue.Created.ToLongDateString();

            return new BaseResponse<IssueViewModel>()
                {
                    Data = issueModel,
                    StatusCode = StatusCode.OK
                }
                ;
        }
        catch (Exception e){
            _logger.LogError($"[IssueService.IssueDetails]: {e.Message}");
            return
                new BaseResponse<IssueViewModel>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = e.Message
                };
        }
    }
    private async Task<List<String>> ReplaceShortcodesWithEmojis(List<String> comments)
    {
        var changedComments = new List<String>();
        foreach (var comment in comments){
            var changedComment = await ReplaceShortcodesWithEmojis(comment);
            changedComments.Add(changedComment);
        }
        return changedComments;
    }

    public async Task<String> ReplaceShortcodesWithEmojis(String comment)
    {
        var regex = ":([a-z_]+):";
        var stringBuilder = new StringBuilder(comment);
        var matches = Regex.Matches(comment, regex);
        if (matches.Count == 0)
            return comment;

        foreach (Match match in matches){
            var key = match
                .Value
                .Replace(":", "")
                .Replace("_", " ");
            var redisValue = await _emojiRepository.Get(key);

            if (!redisValue.IsNull && redisValue.ToString().Length == 0)
                continue;

            if (redisValue.IsNull){
                using var client = new HttpClient();
                var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.api-ninjas.com/v1/emoji?name={key}");
                request.Headers.Add("X-Api-Key", "yz2RIBcZ/VEWQViuds439g==tlunRhKQ6C5vSpzv");
                using var response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK){
                    if (response.Content.Headers.ContentType?.MediaType == "application/json"){
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var jArray = JArray.Parse(jsonString);
                        if (jArray.Count > 0){
                            var jObject = (JObject) jArray[0];
                            var emoji = jObject["character"]!.Value<String>();
                            await _emojiRepository.Create(key, emoji!);
                            stringBuilder.Replace(match.Value, emoji);
                        }
                        else{
                            await _emojiRepository.Create(key, "");
                        }
                    }
                }
                else{
                    throw new BadHttpRequestException("Emoji API is not avaliable");
                }
            }
            else{
                stringBuilder.Replace(match.Value, redisValue.ToString());
            }
        }
        return stringBuilder.ToString();
    }

    public async Task<IBaseResponse<String>> AddComment(String message, Int64 issueId)
    {
        try{
            _logger.LogInformation($"Add comment: {message} to issue: {issueId}");
            var issue = await _issueRepository
                .GetAll()
                .FirstOrDefaultAsync(x => issueId == x.Id);

            if (issue == null)
                return new BaseResponse<String>()
                {
                    Description = "Issue not found",
                    StatusCode = StatusCode.IssueNotFound
                };

            issue.Comments.Add(message);
            await _issueRepository.Update(issue);
            return new BaseResponse<String>()
            {
                Description = $"Comment added - {message} - {issueId}",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e){
            _logger.LogError($"[IssueService.AddComment]: {e.Message}");
            return new BaseResponse<String>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = e.Message
            };
        }

    }
}