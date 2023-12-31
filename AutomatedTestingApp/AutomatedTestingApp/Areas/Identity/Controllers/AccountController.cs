using System.Security.Claims;
using AutomatedTestingApp.Areas.Identity.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTestingApp.Areas.Identity.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string userName, string password, string? returnUrl = null)
    {
        var user = await _userRepository.GetUserByUsernameAsync(userName);
        if (user == null || user.Password != password)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(
                new ClaimsIdentity(
                    new List<Claim>
                    {
                        new(ClaimTypes.Name, user.Username)
                    }, CookieAuthenticationDefaults.AuthenticationScheme)));
        
        return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
    }

    /*[HttpPost]
    public async Task<IActionResult> Register(string userName, string password)
    {
        if (await _userRepository.GetUserByUsernameAsync(userName) != null)
        {
            return BadRequest("User already exist");
        }
        
        _userRepository.CreateUser(new IdentityUser
        {
            UserId = Guid.NewGuid(),
            Username = userName,
            Password = password
        });
        
        _userRepository.Save();
        
        return View("Login");
    }*/
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }
}