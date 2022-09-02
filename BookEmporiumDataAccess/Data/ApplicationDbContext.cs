﻿using Microsoft.EntityFrameworkCore;
using BookEmporium.Models;

namespace BookEmporium.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
    }
}
