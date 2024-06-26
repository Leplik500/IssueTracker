using IssueTracker.DAL.Interfaces;
using IssueTracker.Domain.Enum;
using IssueTracker.Domain.Extensions;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.Issue;
using IssueTracker.Domain.ViewModels.User;
using IssueTracker.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserEntity=IssueTracker.Domain.Entity.UserEntity;

namespace IssueTracker.Service.Implementations;

public class UserService : IUserService {
    private readonly IBaseRepository<UserEntity> _userRepository;
    private ILogger<UserService> _logger;

    public UserService(IBaseRepository<UserEntity> userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<IBaseResponse<UserEntity>> Create(CreateUserViewModel model)
    {
        try{
            _logger.LogInformation($"Create user: {model.Email}");
            var user = await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user != null)
                return new BaseResponse<UserEntity>()
                {
                    Description = "User with this email already exists",
                    StatusCode = StatusCode.UserIsHasAlready
                };

            user = new UserEntity
            {
                Role = model.Role,
                FirstName = model.FirstName,
                LastName = model.LastName,
                // Avatar = model.Avatar,
                Email = model.Email,
                Age = model.Age,
                Created = DateTime.UtcNow,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            await _userRepository.Create(user);
            return new BaseResponse<UserEntity>()
            {
                Description = $"User created - {user.Email} - {user.Created}",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e){
            _logger.LogError($"[UserService.Create]: {e.Message}");
            return new BaseResponse<UserEntity>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = e.Message
            };
        }
    }

    public async Task<IBaseResponse<UserEntity>> Authenticate(CreateUserViewModel model)
    {
        try{
            _logger.LogInformation($"Authenticate user: {model.Email}");
            var user = await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return new BaseResponse<UserEntity>()
                {
                    Description = "User with this email does not exist",
                    StatusCode = StatusCode.UserDoesNotExist
                };

            var passwordVerified = BCrypt.Net.BCrypt.Verify(model.Password, user.HashedPassword);
            if (passwordVerified)
                return new BaseResponse<UserEntity>()
                {
                    Description = $"User authenticated - {user.Email}",
                    StatusCode = StatusCode.OK
                };
            else
                return new BaseResponse<UserEntity>()
                {
                    Description = "Incorrect Password",
                    StatusCode = StatusCode.UserIncorrectPassword
                };
        }
        catch (Exception e){
            _logger.LogError($"[UserService.Authenticate]: {e.Message}");
            return new BaseResponse<UserEntity>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = e.Message
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetAll()
    {
        try{
            var users = await _userRepository
                .GetAll()
                .Select(
                x => new UserViewModel()
                {
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Role = x.Role.GetDisplayName()
                })
                .ToListAsync();

            return
                new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
        }
        catch (Exception e){
            _logger.LogError($"[UserService.GetAll]: {e.Message}");
            return
                new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = e.Message
                };
        }
    }
}