using FlowerApp.Data.DbModels.Trades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder.HasKey(trade => trade.Id);

        builder
            .Property(trade => trade.FlowerName)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(trade => trade.PreferredTrade)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(trade => trade.Location)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(trade => trade.Description)
            .HasMaxLength(500);

        builder
            .Property(trade => trade.CreatedAt)
            .IsRequired();

        builder
            .Property(trade => trade.ExpiresAt)
            .IsRequired();

        builder
            .Property(trade => trade.IsActive)
            .IsRequired();

        builder
            .Property(trade => trade.UserId)
            .IsRequired();

        builder
            .Property(trade => trade.PhotoBase64);
    }
}