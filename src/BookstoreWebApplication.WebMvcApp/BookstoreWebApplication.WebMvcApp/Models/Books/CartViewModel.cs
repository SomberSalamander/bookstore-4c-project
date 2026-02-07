using BookstoreWebApplication.WebMvcApp.Entities;

namespace BookstoreWebApplication.WebMvcApp.Models.Books
{
    public class CartViewModel
    {
        public User User { get; set; }
        public Cart Cart { get; set; }
        public List<CartItemDetailViewModel> CartItems { get; set; }
    }
}
