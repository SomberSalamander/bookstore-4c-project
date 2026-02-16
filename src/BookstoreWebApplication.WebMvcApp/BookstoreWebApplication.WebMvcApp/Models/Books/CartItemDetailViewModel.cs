using BookstoreWebApplication.WebMvcApp.Entities;

namespace BookstoreWebApplication.WebMvcApp.Models.Books
{
    public class CartItemDetailViewModel
    {
        public CartItem CartItem { get; set; }
        public Book Book { get; set; }
    }
}
