using Microsoft.AspNetCore.Mvc;

namespace Notes.API.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}