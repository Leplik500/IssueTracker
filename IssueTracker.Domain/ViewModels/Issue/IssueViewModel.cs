using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.ViewModels.Issue;

public class IssueViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IssuePriority Priority { get; set; }
    public IssueStatus Status { get; set; }

    // public long ProjectId { get; set; }
    // public List<FileInfo> Attachments { get; set; }
    public List<string> Tags { get; set; }
    public List<UserEntity> Assignees { get; set; }
    public List<string> Comments { get; set; }
    public DateTime Created { get; set; }
}