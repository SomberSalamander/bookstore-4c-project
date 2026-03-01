using BookstoreWebApplication.WebMvcApp.Entities;

namespace BookstoreWebApplication.WebMvcApp.Models.Books
{
    public class AdminBookModel
    {
        public bool IsEdited { get; set; }
        public Book Book { get; set; }
        /*
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public string Img { get; set; }
        */
    }
}