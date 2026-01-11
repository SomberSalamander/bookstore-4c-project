using System;
using Microsoft.AspNetCore.Mvc;
using BookstoreWebApplication.WebMvcApp.Entities;

namespace BookstoreWebApplication.WebMvcApp.Controllers
{
    public class BooksController : Controller
    {
        //DbContext
        public List<Book> Books { get; set; }

        public BooksController()
        {
            // DbContext = new BooksDbContext();
            // = DbContext.Books.ToList();
            Books = new List<Book>();
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
    }
}