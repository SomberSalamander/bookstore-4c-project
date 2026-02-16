using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreWebApplication.WebMvcApp.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("userId")]
        public int UserId { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("passwordHash")]
        public string Password { get; set; }

        public User()
        {
        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
