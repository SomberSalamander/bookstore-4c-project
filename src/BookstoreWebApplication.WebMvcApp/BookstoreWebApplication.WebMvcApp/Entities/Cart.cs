using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreWebApplication.WebMvcApp.Entities
{
    [Table("carts")]
    public class Cart
    {
        [Key]
        [Column("cartId")]
        public int CartId { get; set; }
        [Column("userId")]
        public int UserId { get; set; }
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        public Cart()
        {
        }

        public Cart(int cartId, int userId, DateTime createdAt)
        {
            CartId = cartId;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}
