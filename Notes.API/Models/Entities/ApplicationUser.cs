using Notes.API.Enum;
namespace Notes.API.Models.Entities;


public class ApplicationUser
{
    public int ID { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}