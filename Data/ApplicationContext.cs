﻿using Microsoft.EntityFrameworkCore;
using WritersClub.Models;

namespace WritersClub.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
        {




        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
