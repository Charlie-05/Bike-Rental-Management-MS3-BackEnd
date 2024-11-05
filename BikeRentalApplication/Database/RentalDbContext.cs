using BikeRentalApplication.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BikeRentalApplication.Database
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike>()
                .HasMany(b => b.Images)
                .WithOne(i => i.Bike)
                .HasForeignKey(b => b.BikeId);

            modelBuilder.Entity<InventoryUnit>()
                .HasOne(i => i.Bike)
                .WithMany(b => b.InventoryUnits)
                .HasForeignKey(b => b.BikeId);

            modelBuilder.Entity<InventoryUnit>()
              .HasMany(i => i.RentalRecords)
              .WithOne(r => r.InventoryUnit)
              .HasForeignKey(r => r.BikeRegNo);

            modelBuilder.Entity<RentalRecord>()
                .HasOne(r => r.RentalRequest)
                .WithOne(r => r.RentalRecord)
                .HasForeignKey<RentalRecord>(r => r.RentalRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
              .HasMany(u => u.RentalRequests)
              .WithOne(r => r.User)
              .HasForeignKey(r => r.NICNumber);

            modelBuilder.Entity<RentalRecord>()
              .HasOne(r => r.InventoryUnit)
              .WithMany(u => u.RentalRecords)
              .HasForeignKey(r => r.BikeRegNo);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<BikeRentalApplication.Entities.Bike> Bikes { get; set; } = default!;
        public DbSet<BikeRentalApplication.Entities.User> Users { get; set; } = default!;
        public DbSet<BikeRentalApplication.Entities.Image> Images { get; set; } = default!;
        public DbSet<BikeRentalApplication.Entities.InventoryUnit> InventoryUnits { get; set; } = default!;
        public DbSet<BikeRentalApplication.Entities.RentalRequest> RentalRequests { get; set; } = default!;
        public DbSet<BikeRentalApplication.Entities.RentalRecord> RentalRecords { get; set; } = default!;
    }
}
