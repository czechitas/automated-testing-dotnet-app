using System.Security.Claims;
using AutomatedTestingApp.Entity;
using AutomatedTestingApp.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTestingApp.Controllers;

public class AccountController : Controller
{
    readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet]
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
            return View();

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

    [HttpPost]
    public async Task<IActionResult> Register(string userName, string password)
    {
        if (await _userRepository.GetUserByUsernameAsync(userName) != null)
        {
            return BadRequest("User already exist");
        }
        
        _userRepository.CreateUser(new User
        {
            UserId = Guid.NewGuid(),
            Username = userName,
            Password = password
        });
        
        _userRepository.Save();
        
        return View("Login");
    }
    
    public IActionResult AccessDenied(string? returnUrl = null)
    {
        return View();
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }
}