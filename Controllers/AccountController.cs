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

        public AccountController(IUser users)
        {
            _users = users;
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
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _users.GetUserById(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                var userWithSameLogin = await _users.GetUserByLogin(user.Login);
                if (userWithSameLogin != null && userWithSameLogin.Id != user.Id)
                {
                    ModelState.AddModelError("Login", "Этот логин уже занят другим пользователем.");
                    return View(user); 
                }
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Login = user.Login;
                existingUser.Password = user.Password;

                await _users.UpdateUser(existingUser);
                return RedirectToAction(nameof(Index));
            }

            return View(user); 
        }


    }
}
