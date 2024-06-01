using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.ViewModels.User;

public class UserViewModel
{
    public UserRole Role { get; set; }

    // public FileInfo Avatar { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    //TODO: переделать логику получения пользователей как для Issues
}