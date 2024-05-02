using IssueTracker.Domain.Enum;

namespace IssueTracker.Domain.Entity;

public class UserEntity
{
    public UserRole Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public FileInfo Avatar { get; set; }
    public int Age { get; set; }

    public string Email { get; set; }

    // TODO: public UserProfile Profile { get; set; }
    private string Password { get; set; }
}