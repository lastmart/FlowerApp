using FlowerApp.Data.DbModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.GoogleId);

        builder
            .Property(user => user.GoogleId)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(user => user.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(user => user.Surname)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(user => user.Email)
            .HasMaxLength(200);

        builder
            .Property(user => user.Telegram)
            .HasMaxLength(200);

        builder
            .HasMany(user => user.Trades)
            .WithOne(trade => trade.User)
            .HasForeignKey(trade => trade.UserId)
            .HasPrincipalKey(user => user.GoogleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}