namespace Asp.Net.CoreApp.DbContext
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    namespace ConsoleApp.PostgreSQL
    {
        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
          
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("User ID=postgres;Password=04042003;Host=localhost;Port=5432;Database=postgres;");
        }

        public class Blog
        {
            public int BlogId { get; set; }
            public string Url { get; set; }

        }

       
    }
}
