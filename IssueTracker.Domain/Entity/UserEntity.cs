using System.ComponentModel.DataAnnotations;
using IssueTracker.Domain.Enum;

#pragma warning disable CS8618

namespace IssueTracker.Domain.Entity;

public class UserEntity
{
    public UserRole Role { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    // TODO: public FileInfo Avatar { get; set; }
    public int Age { get; set; }
    public DateTime Created { get; set; }

    [Key] public string Email { get; set; }

    // TODO: public UserProfile Profile { get; set; }
    public string HashedPassword { get; set; }
}