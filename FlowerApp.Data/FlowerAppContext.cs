using FlowerApp.Data.Configurations;
using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data;

public class FlowerAppContext : DbContext
{
    public DbSet<Flower> Flowers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<User> Users { get; set; }

    public FlowerAppContext(DbContextOptions<FlowerAppContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FlowerConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new UserAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}