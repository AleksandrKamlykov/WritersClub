using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WritersClub.Models;
using WritersClub.Repository;

namespace WritersClub.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _users;


        public AccountController(IUser users)
        {
            _users = users;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _users.GetAllUsers();
            ViewData["Login"] = TempData["Login"]?.ToString() ?? string.Empty;
            ViewData["Name"] = TempData["Name"]?.ToString() ?? string.Empty;
            return View(users);
        }
        public IActionResult Account()
        {
            return View();
        }
        public async Task<IActionResult> IsUserNameAvailable(string login)
        {
            bool isAvailable = await _users.IsUserNameAvailable(login);
            if (!isAvailable)
            {
                return Json("Имя пользователя уже занято.");
            }
            return Json(true);
        }
        [HttpPost]
        public async Task<IActionResult> Account(User user)
        {
            if (ModelState.IsValid)
            {
                await _users.AddUser(user);
                TempData["Login"] = user.Login;
                TempData["Name"] = user.Name;
                return RedirectToAction(nameof(Index)); 
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _users.DeleteUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
