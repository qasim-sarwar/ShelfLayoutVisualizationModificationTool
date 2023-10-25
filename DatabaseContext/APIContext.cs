using TexCode.ViewModels;
using Microsoft.EntityFrameworkCore;
using TexCode.Entities;

namespace TexCode.DatabaseContext
{
    public class APIContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TexInMemoryDb");
            base.OnConfiguring(optionsBuilder); // Call the base method
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().HasNoKey();
            modelBuilder.Entity<Size>().HasNoKey();

            // Ignore the Size property in the Cabinet entity
            modelBuilder.Entity<Cabinet>().Ignore(c => c.Size);

            // Define other entity configurations

            // ... Define other relationships and configurations

            // Define primary keys for other entities
            modelBuilder.Entity<ShelfLayout>().HasNoKey();
            modelBuilder.Entity<Cabinet>().HasKey(e => e.CabinetId);
            modelBuilder.Entity<Row>().HasKey(e => e.RowId);
            modelBuilder.Entity<Lane>().HasKey(e => e.LaneId);
            modelBuilder.Entity<SKU>().HasKey(e => e.JanCode);
        }

        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<ShelfLayout> Shelves { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SKU> SKUs { get; set; }
    }
}
