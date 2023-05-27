using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Notes.API.Enum;
using Notes.API.Helpers;
using Notes.API.Interfaces;
using Notes.API.Models.Entities;
using Notes.API.Response;
using Notes.API.ViewModels;

namespace Notes.API.Services;

public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        
        private readonly IBaseRepository<ApplicationUser> _userRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<ApplicationUser> userRepository
            )
        {
            _logger = logger;
            _userRepository = userRepository;
            
        }

        public async Task<IBaseResponse<ApplicationUser>> Create(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
                if (user != null)
                {
                    return new BaseResponse<ApplicationUser>()
                    {
                        Description = "User with same login already exists",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }
                user = new ApplicationUser()
                {
                    Login = model.Login,
                    Role = Role.Parse<Role>(model.Role),
                    Password = HashPasswordHelper.HashPassword(model.Password),
                };
                
                await _userRepository.Create(user);
                
                
                
               
                
                return new BaseResponse<ApplicationUser>()
                {
                    Data = user,
                    Description = "User added successfully",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<ApplicationUser>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Internal error: {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[]) Role.GetValues(typeof(Role)))
                    .ToDictionary(k => (int) k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.ID,
                        Login = x.Login,
                        Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

                _logger.LogInformation($"[UserService.GetUsers] got elements {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Internal error: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.ID == id);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userRepository.Delete(user);
                _logger.LogInformation($"[UserService.DeleteUser] user deleted");
                
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Internal error: {ex.Message}"
                };
            }
        }
    }