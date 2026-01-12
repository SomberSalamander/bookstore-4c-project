using System;
using Microsoft.AspNetCore.Mvc;
using BookstoreWebApplication.WebMvcApp.Entities;
using Microsoft.EntityFrameworkCore;
using BookstoreWebApplication.WebMvcApp.Data;

namespace BookstoreWebApplication.WebMvcApp.Controllers
{
    public class BooksController : Controller
    {
        public BooksDbContext DbContext { get; set; }
        public List<Book> Books { get; set; }

        public BooksController()
        {
            DbContext = new BooksDbContext();
            Books = DbContext.Books.ToList();
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

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }
    }
}