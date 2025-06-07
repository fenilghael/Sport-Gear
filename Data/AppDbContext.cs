using Microsoft.EntityFrameworkCore;
using SportsGearCMS.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SportsGearCMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<GearItem> GearItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<GearItemCategory> GearItemCategories { get; set; }
        public DbSet<GearItemManufacturer> GearItemManufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // GearItemCategory composite key
            modelBuilder.Entity<GearItemCategory>()
                .HasKey(gc => new { gc.GearItemId, gc.CategoryId });

            modelBuilder.Entity<GearItemCategory>()
                .HasOne(gc => gc.GearItem)
                .WithMany(g => g.GearItemCategories)
                .HasForeignKey(gc => gc.GearItemId);

            modelBuilder.Entity<GearItemCategory>()
                .HasOne(gc => gc.Category)
                .WithMany(c => c.GearItemCategories)
                .HasForeignKey(gc => gc.CategoryId);

            // GearItemManufacturer composite key
            modelBuilder.Entity<GearItemManufacturer>()
                .HasKey(gm => new { gm.GearItemId, gm.ManufacturerId });

            modelBuilder.Entity<GearItemManufacturer>()
                .HasOne(gm => gm.GearItem)
                .WithMany(g => g.GearItemManufacturers)
                .HasForeignKey(gm => gm.GearItemId);

            modelBuilder.Entity<GearItemManufacturer>()
                .HasOne(gm => gm.Manufacturer)
                .WithMany(m => m.GearItemManufacturers)
                .HasForeignKey(gm => gm.ManufacturerId);
        }
    }
}
