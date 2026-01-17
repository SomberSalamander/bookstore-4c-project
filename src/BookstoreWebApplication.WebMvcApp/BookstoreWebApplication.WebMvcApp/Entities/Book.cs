using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreWebApplication.WebMvcApp.Entities
{
    [Table("books")]
    public class Book
    {
        [Key]
        [Column("bookId")]
        public int BookId { get; set; }
        
        [Column("title")]
        public string Title { get; set; }
        
        [Column("author")]
        public string Author { get; set; }
        
        [Column("publisher")]
        public string Publisher { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        // CZK
        [Column("price")]
        public float Price { get; set; }
        
        [Column("stock")]
        public int Stock { get; set; }

        [Column("img")]
        public string Img { get; set; }

        public Book()
        {
        }

        public Book(int bookId, string title, string author, string publisher, string description, float price, int stock, string img)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            Publisher = publisher;
            Description = description;
            Price = price;
            Stock = stock;
            Img = img;
        }
    }
}