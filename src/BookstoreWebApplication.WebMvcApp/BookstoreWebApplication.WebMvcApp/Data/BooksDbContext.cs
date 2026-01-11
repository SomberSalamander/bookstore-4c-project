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
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=4c1_volsickabarbora_db1;user=volsickabarbora;password=123456");
        }
    }
}