using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.Entity;

public class IssueEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IssuePriority Priority { get; set; }
    public IssueStatus Status { get; set; }

    // TODO:
    // public long ProjectId { get; set; }
    // public List<FileInfo> Attachments { get; set; }
    public List<string> Tags { get; set; }
    public List<UserEntity> Assignees { get; set; }
    public List<string> Comments { get; set; }
    public DateTime Created { get; set; }
}