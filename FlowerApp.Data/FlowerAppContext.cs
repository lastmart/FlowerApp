using FlowerApp.Data.Configurations;
using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data;

public class FlowerAppContext : DbContext
{
    public DbSet<Flower> Flowers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Trade> Trades { get; set; }

    public FlowerAppContext(DbContextOptions<FlowerAppContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new FlowerConfiguration());
        modelBuilder.ApplyConfiguration(new TradeConfiguration());
    }
}