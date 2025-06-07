using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace contact_management_winforms
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Store the database file in the application's base directory
            // For a real application, consider a user-specific directory or a configurable path.
            var dbPath = Path.Combine(AppContext.BaseDirectory, "contacts.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}