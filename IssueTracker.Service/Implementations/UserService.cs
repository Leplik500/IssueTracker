using System.Security.Cryptography;
using CryptoHelper;
using IssueTracker.DAL.Interfaces;
using IssueTracker.Domain.Entity;
using IssueTracker.Domain.Enum;
using IssueTracker.Domain.Response;
using IssueTracker.Domain.ViewModels.User;
using IssueTracker.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserEntity = IssueTracker.Domain.Entity.UserEntity;

namespace IssueTracker.Service.Implementations;

public class UserService : IUserService
{
    private readonly IBaseRepository<UserEntity> _userRepository;
    private ILogger<UserService> _logger;

    public UserService(IBaseRepository<UserEntity> userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<IBaseResponse<UserEntity>> Create(CreateUserViewModel model)
    {
        try
        {
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
                Salt = GenerateSalt(),
                Age = model.Age,
                Created = DateTime.UtcNow
            };
            user.PasswordAndSaltHash = Crypto.HashPassword(String.Concat(model.Password, user.Salt));

            await _userRepository.Create(user);
            return new BaseResponse<UserEntity>()
            {
                Description = $"User created - {user.Email} - {user.Created}",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"[UserService.Create]: {e.Message}");
            return new BaseResponse<UserEntity>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = e.Message
            };
        }
    }

    private string GenerateSalt(int saltLength = 16)
    {
        byte[] randomBytes = new byte[saltLength];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return Convert.ToBase64String(randomBytes);
    }
}