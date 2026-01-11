using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreWebApplication.WebMvcApp.Entities
{
    public class Book
    {
        [Key]
        [Column("bookId")]
        public int BookId { get; set; }
        
        [Column("title")]
        public string Title { get; set; }
        
        [Column("author")]
        public int Author { get; set; }
        
        [Column("publisher")]
        public int Publisher { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("price")]
        public float Price { get; set; }
        
        [Column("stock")]
        public int Stock { get; set; }
        
        [Column("publishedAt")]
        public DateTime PublishedAt { get; set; }
        
        [Column("cover")]
        public CoverType Cover { get; set; }
        
        [Column("language")]
        public string Language { get; set; }
        
        [Column("isbn")]
        public string ISBN { get; set; }
        
        [Column("numberOfPages")]
        public int NumberOfPages { get; set; }
        
        [Column("rating")]
        // 0 - 5
        public float Rating { get; set; }
}

    public enum CoverType { Hard, Soft, Papercover }
}