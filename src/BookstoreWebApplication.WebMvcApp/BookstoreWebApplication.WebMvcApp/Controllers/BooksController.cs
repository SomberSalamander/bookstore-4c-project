using BookstoreWebApplication.WebMvcApp.Data;
using BookstoreWebApplication.WebMvcApp.Entities;
using BookstoreWebApplication.WebMvcApp.Models.Auth;
using BookstoreWebApplication.WebMvcApp.Models.Books;
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
        public List<CartItem> CartItems { get; set; }

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
        
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Book book = Books.First(b => b.BookId == id);
            return View(book);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Cart()
        {
            var userId = User.FindFirstValue("id");
            if (userId == null) return Unauthorized();
            var user = DbContext.Users.FirstOrDefault(u => u.UserId == Convert.ToInt32(userId));
            if (user == null) return NotFound("User not found.");
            var cart = DbContext.Carts.FirstOrDefault(c => c.UserId == Convert.ToInt32(userId));
            if (cart == null) return NotFound("Cart not found.");
            var cartItems = DbContext.CartItems
                .Where(ci => ci.CartId == cart.CartId)
                .ToList();

            var cartItemDetails = cartItems
                .Select(ci => new CartItemDetailViewModel
                {
                    CartItem = ci,
                    Book = DbContext.Books.FirstOrDefault(b => b.BookId == ci.BookId)
                })
                .ToList();

            var viewModel = new CartViewModel
            {
                User = user,
                Cart = cart,
                CartItems = cartItemDetails
            };

            return View(viewModel);
        }
    }
}