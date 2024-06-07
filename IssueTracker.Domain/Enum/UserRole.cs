using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Domain.Enum;

public enum UserRole {
    [Display(Name = "Admin")]
    Admin = 0,

    [Display(Name = "Manager")]
    Manager = 1,

    [Display(Name = "Developer")]
    Developer = 2,

    [Display(Name = "Tester")]
    Tester = 3,

    [Display(Name = "Guest")]
    Guest = 4,

    [Display(Name = "Product Owner")]
    ProductOwner = 5
}