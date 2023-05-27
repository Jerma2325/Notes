using System.ComponentModel.DataAnnotations;

namespace Notes.API.ViewModels;

public class UserViewModel
{
    [Display(Name = "Id")]
    public long Id { get; set; }
        
    [Required(ErrorMessage = "Set role")]
    [Display(Name = "Role")]
    public string Role { get; set; }
        
    [Required(ErrorMessage = "Set login")]
    [Display(Name = "Login")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Set password")]
    [Display(Name = "Password")]
    public string Password { get; set; }
}