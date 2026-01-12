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
        
        [Column("publishedAt")]
        public DateTime PublishedAt { get; set; }
        
        [Column("cover")]
        public string Cover { get; set; }
        
        [Column("language")]
        public string Language { get; set; }
        
        [Column("isbn")]
        public string ISBN { get; set; }
        
        [Column("numberOfPages")]
        public int NumberOfPages { get; set; }

        // 0 - 5
        [Column("rating")]
        public float Rating { get; set; }

        public Book()
        {
        }

        public Book(int bookId, string title, string author, string publisher, string description, float price, int stock, DateTime publishedAt, string cover, string language, string isbn, int numberOfPages, float rating)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            Publisher = publisher;
            Description = description;
            Price = price;
            Stock = stock;
            PublishedAt = publishedAt;
            Cover = cover;
            Language = language;
            ISBN = isbn;
            NumberOfPages = numberOfPages;
            Rating = rating;
        }
    }
}