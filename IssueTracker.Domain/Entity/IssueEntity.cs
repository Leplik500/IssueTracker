using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.Entity;

public class IssueEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IssuePriority IssuePriority { get; set; }
    public IssueStatus IssueStatus { get; set; }
    public long ProjectId { get; set; }
    public List<FileInfo> Attachments { get; set; }
    public List<string> tags { get; set; }
    public List<UserEntity> Assignees { get; set; }
    public List<string> Comments { get; set; }
}