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

        [Authorize]
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

        [HttpGet]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var cartItem = DbContext.CartItems.Find(cartItemId);
            if (cartItem != null)
            {
                DbContext.CartItems.Remove(cartItem);
                DbContext.SaveChanges();
            }
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public IActionResult AddToCart(int bookId)
        {
            var userId = User.FindFirstValue("id");
            if (userId == null) return Unauthorized();
            var user = DbContext.Users.FirstOrDefault(u => u.UserId == Convert.ToInt32(userId));
            if (user == null) return NotFound("User not found.");
            var cart = DbContext.Carts.FirstOrDefault(c => c.UserId == Convert.ToInt32(userId));
            if (cart == null) return NotFound("Cart not found.");
            int cartId = cart.CartId;

            var existingCartItem = DbContext.CartItems.FirstOrDefault(ci => ci.CartId == cartId && ci.BookId == bookId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                CartItem cartItem = new CartItem(cartId, bookId, 1);
                DbContext.CartItems.Add(cartItem);
            }

            DbContext.SaveChanges();

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            var cartItem = DbContext.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
            if (cartItem != null && quantity > 0)
            {
                cartItem.Quantity = quantity;
                DbContext.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminList()
        {
            return View(Books);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteBook(int bookId)
        {
            var book = DbContext.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book != null)
            {
                DbContext.Books.Remove(book);
                List<CartItem> allCartItems = DbContext.CartItems.Where(ci => ci.BookId == bookId).ToList();

                foreach (var ci in allCartItems)
                {
                    DbContext.CartItems.Remove(ci);
                }

                DbContext.SaveChanges();
            }

            return RedirectToAction("AdminList");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminBook(bool isEdited, int bookId)
        {
            Book book = DbContext.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book == null)
            {
                book = new Book();
            }

            AdminBookModel model = new AdminBookModel();
            model.IsEdited = isEdited;
            model.Book = book;

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AdminBook(AdminBookModel adminBookModel)
        {
            if (adminBookModel.IsEdited)
            {
                var book = DbContext.Books.FirstOrDefault(b => b.BookId == adminBookModel.Book.BookId);
                if (book != null)
                {
                    book.Title = adminBookModel.Book.Title;
                    book.Author = adminBookModel.Book.Author;
                    book.Publisher = adminBookModel.Book.Publisher;
                    book.Description = adminBookModel.Book.Description;
                    book.Price = adminBookModel.Book.Price;
                    book.Stock = adminBookModel.Book.Stock;
                    book.Img = adminBookModel.Book.Img;
                    DbContext.SaveChanges();
                }
            }
            else
            {
                Book newBook = new Book(
                    adminBookModel.Book.Title,
                    adminBookModel.Book.Author,
                    adminBookModel.Book.Publisher,
                    adminBookModel.Book.Description,
                    adminBookModel.Book.Price,
                    adminBookModel.Book.Stock,
                    adminBookModel.Book.Img
                );
                DbContext.Books.Add(newBook);
                DbContext.SaveChanges();
            }

            return RedirectToAction("AdminList", Books);
        }
    }
}