using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WritersClub.Auth;
using WritersClub.Models;
using WritersClub.Repository;
using WritersClub.ViewModel;

public class AuthController : Controller
{
    private readonly TokenService _tokenService;
    private readonly PasswordHasherService _passwordHasherService;
    private readonly IUser _userRepository;

    public AuthController(TokenService tokenService, PasswordHasherService passwordHasherService, IUser userRepository)
    {
        _tokenService = tokenService;
        _passwordHasherService = passwordHasherService;
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Register(User model)
    {
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPost(User model)
    {
        if (!ModelState.IsValid)
        {
            return View("Register", model);
        }

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Login = model.Login,
            State = UserState.Active
        };

        user.Password = _passwordHasherService.HashPassword(user, model.Password);
        await _userRepository.AddUser(user);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        var user = await _userRepository.GetUserByLogin(model.Username);
        if (user == null || _passwordHasherService.VerifyPassword(user, user.Password, model.Password) != PasswordVerificationResult.Success)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Index", model);
        }

        var token = _tokenService.GenerateToken(user);

        Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        });

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
