using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Domain.Enum;

public enum IssueStatus
{
    [Display(Name = "New")] New = 0,
    [Display(Name = "Assigned")] Assigned = 1,
    [Display(Name = "In Progress")] InProgress = 2,
    [Display(Name = "Fixed")] Fixed = 3,
    [Display(Name = "Closed")] Closed = 4,
    [Display(Name = "Reopened")] ReOpened = 5,
    [Display(Name = "Deferred")] Deferred = 6,
    [Display(Name = "Duplicate")] Duplicate = 7,
    [Display(Name = "Rejected")] Rejected = 8
}