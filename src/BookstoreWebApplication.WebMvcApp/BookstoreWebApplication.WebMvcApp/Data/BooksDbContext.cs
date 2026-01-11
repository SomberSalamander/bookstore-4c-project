using System;
using Microsoft.EntityFrameworkCore;
using BookstoreWebApplication.WebMvcApp.Entities;

namespace BookstoreWebApplication.WebMvcApp.Data
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=mrkacek_db1;user=mrkacekread;password=123456");
        }
    }
}