namespace IssueTracker.Domain.Entity;

public class IssueEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public long ProjectId { get; set; }
    public List<FileInfo> Attachments { get; set; }
    public List<String> tags { get; set; }
    public List<User> Assignees { get; set; }
    public List<String> Comments { get; set; }
}