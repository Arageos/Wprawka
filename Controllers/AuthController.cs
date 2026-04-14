using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamePlatform.Data;
using GamePlatform.Models;
using System.Security.Cryptography;
using System.Text;

namespace GamePlatform.Controllers
{
    public class AuthController : Controller
    {
        private readonly GamePlatformContext _context;

        public AuthController(GamePlatformContext context)
        {
            _context = context;
        }

        // GET: Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Sprawdź czy login już istnieje
            bool loginExists = await _context.AppUser
                .AnyAsync(u => u.Username == model.Username);
            if (loginExists)
            {
                ModelState.AddModelError("Username", "Ten login jest już zajęty");
                return View(model);
            }

            // Sprawdź czy email już istnieje
            bool emailExists = await _context.AppUser
                .AnyAsync(u => u.Email == model.Email);
            if (emailExists)
            {
                ModelState.AddModelError("Email", "Ten email jest już używany");
                return View(model);
            }

            var user = new AppUser
            {
                Username = model.Username,
                PasswordHash = HashPassword(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            _context.AppUser.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.AppUser
                .FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null || user.PasswordHash != HashPassword(model.Password))
            {
                ModelState.AddModelError("", "Nieprawidłowy login lub hasło");
                return View(model);
            }

            // Zapisz dane użytkownika w ciasteczku
            Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });
            Response.Cookies.Append("Username", user.Username, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });

            return RedirectToAction("Index", "Home");
        }

        // GET: Auth/Logout
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("Username");
            return RedirectToAction("Index", "Home");
        }

        // Hashowanie SHA256
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}