namespace IssueTracker.Domain.Enum;

public enum StatusCode
{
    UserIsHasAlready = 1,
    UserDoesNotExist = 2,
    UserIncorrectPassword = 3,
    IssueIsHasAlready = 4,
    OK = 200,
    InternalServerError = 500
}