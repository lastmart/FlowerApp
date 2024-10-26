using FlowerApp.Data.Configurations;
using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data;

public class FlowerAppContext : DbContext
{
    public DbSet<Flower> Flowers { get; set; }
    public DbSet<LightParameters> LightParameters { get; set; }

    public FlowerAppContext(DbContextOptions<FlowerAppContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FlowerConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}