using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.User;
using IssueTracker.Service.Interface;

namespace IssueTracker.Service.Implementations;

public class UserService : IUserService
{
    public Task<IBaseResponse<UserEntity>> Create(CreateUserViewModel model)
    {
        throw new NotImplementedException();
    }
}