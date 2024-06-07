using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.ViewModels.User;

public class UserViewModel {
    public String Role { get; set; }

    // public FileInfo Avatar { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }

    public String Email { get; set; }
}