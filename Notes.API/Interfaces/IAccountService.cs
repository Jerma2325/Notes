using System.Security.Claims;
using Notes.API.Response;
using Notes.API.ViewModels;

namespace Notes.API.Interfaces;

public interface IAccountService
{
    
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        
    
}