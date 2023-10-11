
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(p => p.DiscountRate)
           .HasDefaultValue(0.15m)
           .IsRequired();
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories  { get; set; }

}
