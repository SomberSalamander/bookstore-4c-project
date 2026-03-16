using BookstoreWebApplication.WebMvcApp.Data;
using BookstoreWebApplication.WebMvcApp.Entities;
using BookstoreWebApplication.WebMvcApp.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace BookstoreWebApplication.WebMvcApp.Controllers
{

    public class AuthController : Controller
    {
        public BooksDbContext DbContext { get; set; }

        public AuthController()
        {
            DbContext = new BooksDbContext();
        }

        
        private async Task SignInUser(User user)
        {
            List<Claim> claims = new List<Claim>();

            Claim idClaim = new Claim("id", user.UserId.ToString());
            Claim emailClaim = new Claim("email", user.Email);
            Claim roleClaim = new Claim(ClaimTypes.Role, user.Role);

            claims.Add(idClaim);
            claims.Add(emailClaim);
            claims.Add(roleClaim);

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        // pokud je asynchronni, jen se obali promenna v Task<>
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //1. Zkontrolovat, ze uzivatel vubec existuje
            User? user = DbContext.Users
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                return View(model);
            }

            //2. Sestavit "identitu/totoznost" uzivatele pomoci Claims
            await SignInUser(user);

            return RedirectToAction("List", "Books");
        }

        [HttpGet]
        public IActionResult Register()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (DbContext.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            User newUser = new User(model.Email, model.Password, "User");
            DbContext.Users.Add(newUser);
            DbContext.SaveChanges();

            Cart newCart = new Cart(newUser.UserId);
            DbContext.Carts.Add(newCart);
            DbContext.SaveChanges();

            await SignInUser(newUser);

            return RedirectToAction("List", "Books");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Denied() 
        {
            return View(); 
        }
    }
}