using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.ViewModels.User;

public class CreateUserViewModel
{
    public UserRole Role { get; set; }

    // public FileInfo Avatar { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public int Age { get; set; }
    //TODO: создать UserViewModel
    //TODO: переделать GetIssues в контроллере как в видео Домоседа
    //TODO: переделать логику получения пользователей как для Issues
}