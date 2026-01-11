using System;

public class Book
{
    public class Book
    {
        // PK
        public int BookID { get; set; }
        
        public string Title { get; set; }

        public int Author { get; set; }

        public int Publisher { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public int Stock { get; set; }

        public DateTime PublishedAt { get; set; }

        // enum
        public CoverType Cover { get; set; }

        public DateTime Edition { get; set; }

        public string Language { get; set; }

        public string ISBN { get; set; }

        // examp. "20cm x 13cm x 2cm"
        public string Dimensions { get; set; }

        // kg
        public double Weight { get; set; }

        public int NumberOfPages { get; set; }

        // 0 - 5
        public float Rating { get; set; }

        // optional
        public int? TranslatorID { get; set; }
        public int? IllustratorID { get; set; }
    }

    public enum CoverType { Hard, Soft, Papercover }
}
