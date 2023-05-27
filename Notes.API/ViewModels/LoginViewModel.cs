using System.ComponentModel.DataAnnotations;

namespace Notes.API.ViewModels;

public class LoginViewModel
{
    
    public string Login { get; set; }

    
    [DataType(DataType.Password)]
    
    public string Password { get; set; }
}