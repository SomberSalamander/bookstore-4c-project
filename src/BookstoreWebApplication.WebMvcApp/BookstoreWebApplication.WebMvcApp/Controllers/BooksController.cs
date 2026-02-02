using BookstoreWebApplication.WebMvcApp.Data;
using BookstoreWebApplication.WebMvcApp.Entities;
using BookstoreWebApplication.WebMvcApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace BookstoreWebApplication.WebMvcApp.Controllers
{
    
    public class BooksController : Controller
    {
        public BooksDbContext DbContext { get; set; }
        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }
        public List<Cart> Carts { get; set; }

        public BooksController()
        {
            DbContext = new BooksDbContext();
            Books = DbContext.Books.ToList();
            Users = DbContext.Users.ToList();
            Carts = DbContext.Carts.ToList();
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(Books);
        }
        [Authorize]

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Book book = Books.First(b => b.BookId == id);
            return View(book);
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
            User? user = Users
                .FirstOrDefault(u => u.Email == model.Email && u.PasswordHash == model.Password);

            if (user == null)
            {
                return View(model);
            }

            //2. Sestavit "identitu/totoznost" uzivatele pomoci Claims
            List<Claim> claims = new List<Claim>();

            Claim idClaim = new Claim("id", user.UserId.ToString());
            Claim emailClaim = new Claim("email", user.Email);
            
            claims.Add(idClaim);
            claims.Add(emailClaim);

            // "prasacky jen "Cookie" "
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // na asynchronni se musi cekat
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            return RedirectToAction("Cart", "Books");
        }

        [HttpGet]
        public IActionResult Register()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel model)
        {
            // TODO: vytvorit noveho uzivatele a pridat do DB
            return View();
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View(Cart);
        }
    }
}