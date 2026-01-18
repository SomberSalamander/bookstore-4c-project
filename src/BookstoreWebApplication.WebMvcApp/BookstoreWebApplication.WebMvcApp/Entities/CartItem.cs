using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreWebApplication.WebMvcApp.Entities
{
    [Table("cartItems")]
    public class CartItem
    {
        [Key]
        [Column("cartItemId")]
        public int CartItemId { get; set; }
        [Column("cartId")]
        public int CartId { get; set; }
        [Column("bookId")]
        public int BookId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }

        public CartItem()
        {
        }

        public CartItem(int cartItemId, int cartId, int bookId, int quantity)
        {
            CartItemId = cartItemId;
            CartId = cartId;
            BookId = bookId;
            Quantity = quantity;
        }
    }
}
