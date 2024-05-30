namespace IssueTracker.Domain.Enum;

public enum StatusCode
{
    UserIsHasAlready = 1,
    UserDoesNotExist = 2,
    IncorrectPassword = 3,
    OK = 200,
    InternalServerError = 500
}