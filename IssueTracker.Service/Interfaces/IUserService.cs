using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.User;

namespace IssueTracker.Service.Interface;

public interface IUserService
{
    Task<IBaseResponse<UserEntity>> Create(CreateUserViewModel model);
}