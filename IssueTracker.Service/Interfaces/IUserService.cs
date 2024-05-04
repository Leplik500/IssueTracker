using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.User;

namespace IssueTracker.Service.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<UserEntity>> Create(CreateUserViewModel model);
}