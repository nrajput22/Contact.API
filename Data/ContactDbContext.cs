using Microsoft.EntityFrameworkCore;

namespace Contact.API.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) 
            :base(options)
        {

        }

        public DbSet<Contact.API.Entities.Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for the Contact model
            modelBuilder.Entity<Contact.API.Entities.Contact>().HasData(
                new Contact.API.Entities.Contact
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    Phone = "1234567890",
                    Favorite = true
                },
                new Contact.API.Entities.Contact
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "janesmith@example.com",
                    Phone = "9876543210",
                    Favorite = false
                },
                new Contact.API.Entities.Contact
                {
                    Id = 3,
                    Name = "Mike Brown",
                    Email = "mikebrown@example.com",
                    Phone = "4567891230",
                    Favorite = true
                }
            );
        }
    }
}
