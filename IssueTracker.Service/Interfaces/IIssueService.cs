using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.Issue;

namespace IssueTracker.Service.Interfaces;

public interface IIssueService {
    Task<IBaseResponse<IssueEntity>> Create(CreateIssueViewModel model);
    Task<IBaseResponse<IEnumerable<IssueViewModel>>> GetAll();
    Task<IBaseResponse<IssueViewModel>> GetIssue(Int64 id);
    Task<IBaseResponse<String>> AddComment(String message, Int64 issueId);
}