using FlowerApp.Data.Configurations;
using FlowerApp.Data.DbModels.Trades;
using FlowerApp.Data.DbModels.Flowers;
using FlowerApp.Data.DbModels.Surveys;
using FlowerApp.Data.DbModels.Users;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Data;

public class FlowerAppContext : DbContext
{
    public DbSet<Flower> Flowers { get; set; }
    public DbSet<SurveyQuestion> Questions { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public DbSet<SurveyFlower> SurveyFlowers { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Trade> Trades { get; set; }
    public DbSet<User> Users { get; set; }

    public FlowerAppContext(DbContextOptions<FlowerAppContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new FlowerConfiguration())
            .ApplyConfiguration(new QuestionConfiguration())
            .ApplyConfiguration(new SurveyAnswerConfiguration())
            .ApplyConfiguration(new SurveyFlowerConfiguration())
            .ApplyConfiguration(new SurveyConfiguration())
            .ApplyConfiguration(new TradeConfiguration())
            .ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}