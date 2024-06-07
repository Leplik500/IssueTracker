using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Domain.Enum;

public enum IssuePriority
{
    [Display(Name = "Critical")] Critical = 0,
    [Display(Name = "High")] High = 1,
    [Display(Name = "Medium")] Medium = 2,
    [Display(Name = "Low")] Low = 3,
    [Display(Name = "Enhancement")] Enhancement = 4
}