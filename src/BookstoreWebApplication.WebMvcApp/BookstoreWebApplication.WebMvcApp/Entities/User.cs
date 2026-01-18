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
        [Column("name")]
        public string Name { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("passwordHash")]
        public string PasswordHash { get; set; }

        public User()
        {
        }

        public User(int userId, string name, string email, string passwordHash)
        {
            UserId = userId;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
