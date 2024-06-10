using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.Entity;

public class IssueEntity {
    public Int64 Id { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public IssuePriority Priority { get; set; }
    public IssueStatus Status { get; set; }

    // TODO:
    // public long ProjectId { get; set; }
    // public List<FileInfo> Attachments { get; set; }
    // public UserEntity Author { get; set; }
    public List<String> Tags { get; set; }
    public List<UserEntity> Assignees { get; set; }
    public List<String> Comments { get; set; }
    public DateTime Created { get; set; }
}