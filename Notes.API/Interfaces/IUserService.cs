using Notes.API.Models.Entities;
using Notes.API.Response;
using Notes.API.ViewModels;

namespace Notes.API.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<ApplicationUser>> Create(UserViewModel model);
        
    BaseResponse<Dictionary<int, string>> GetRoles();
        
    Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
        
    Task<IBaseResponse<bool>> DeleteUser(long id);
}