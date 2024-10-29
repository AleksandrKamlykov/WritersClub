using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WritersClub.Models;
using WritersClub.Repository;

namespace WritersClub.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _users;
        private readonly IAntiforgery _antiforgery;

        public AccountController(IUser users, IAntiforgery antiforgery)
        {
            _users = users;
            _antiforgery = antiforgery;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _users.GetAllUsers();
            return View(users);
        }
        public IActionResult Account()
        {
            return View();
        }
        public async Task<IActionResult> IsUserNameAvailable(string login,int id)
        {
            var userWithSameLogin = await _users.GetUserByLogin(login);

     
            if (userWithSameLogin != null && userWithSameLogin.Id != id)
            {
                return Json("Этот логин уже занят другим пользователем.");
            }

            return Json(true);
        }
        [HttpPost]
        public async Task<IActionResult> Account(User user)
        {
            if (ModelState.IsValid)
            {
                await _users.AddUser(user);
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
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await _users.GetUserById(id); 
            if (user == null)
            {
                return NotFound();
            }
            return View(user); 
        }
        [HttpPost]
        public async Task<IActionResult>UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                await _users.UpdateUser(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
