using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.ViewModels.Issue;

public class CreateIssueViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IssuePriority Priority { get; set; }

    // public long ProjectId { get; set; }
    // public List<FileInfo> Attachments { get; set; }
    public List<string> Tags { get; set; }
    public List<string> AvaliableUsers { get; set; }
}