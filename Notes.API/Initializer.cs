using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Notes.API.Interfaces;
using Notes.API.Models.Entities;
using Notes.API.Repositories;
using Notes.API.Services;

namespace Notes.API;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<ApplicationUser>, UserRepository>();
        //services.AddScoped<ITempDataDictionaryFactory, TempDataDictionaryFactory>();
        
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();
        
    }
}