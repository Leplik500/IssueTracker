using System.ComponentModel.DataAnnotations;
using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Enum;
using IssueTracker.Domain.ViewModels.User;

namespace IssueTracker.Domain.ViewModels.Issue;

public class IssueViewModel {
    [Display(Name = "Title")] public String Title { get; set; }
    [Display(Name = "Description")] public String Description { get; set; }
    [Display(Name = "Priority")] public String Priority { get; set; }
    [Display(Name = "Status")] public String Status { get; set; }

    // public long ProjectId { get; set; }
    // public List<FileInfo> Attachments { get; set; }
    [Display(Name = "Tags")] public List<String> Tags { get; set; }
    [Display(Name = "Assignees")] public List<UserViewModel> Assignees { get; set; }
    [Display(Name = "Comments")] public List<String> Comments { get; set; }
    [Display(Name = "Created")] public String Created { get; set; }
}