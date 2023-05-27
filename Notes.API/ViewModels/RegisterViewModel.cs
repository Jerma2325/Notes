using System.ComponentModel.DataAnnotations;

namespace Notes.API.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Login is required")]
    [MaxLength(20, ErrorMessage = "less than 20 characters")]
    [MinLength(3, ErrorMessage = "Login must contain at least 3 characters")]
    public string Login { get; set; }
        
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Give password")]
    [MinLength(6, ErrorMessage = "Password must contain at least 6 characters ")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password confirmation")]
    [Compare("Password", ErrorMessage = "Passwords doesnt match")]
    public string PasswordConfirm { get; set; }
}