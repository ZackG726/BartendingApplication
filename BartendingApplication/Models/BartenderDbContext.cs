using Microsoft.EntityFrameworkCore;

namespace BartendingApplication.Models
{
    public class BartenderDbContext : DbContext
    {
        // Constructor to pass DbContextOptions to the base class
        public BartenderDbContext(DbContextOptions<BartenderDbContext> options)
            : base(options)
        {
        }

        // DbSets for accessing the database tables
        public DbSet<CocktailMenu> CocktailMenus { get; set; }
        public DbSet<CocktailOrder> CocktailOrders { get; set; }
        public DbSet<User> Users { get; set; }

        // Configurations and constraints for the database model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the precision and scale for the Price property
            modelBuilder.Entity<CocktailMenu>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
