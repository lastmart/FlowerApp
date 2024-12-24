using FlowerApp.Data.DbModels.Flowers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class FlowerConfiguration : IEntityTypeConfiguration<Flower>
{
    public void Configure(EntityTypeBuilder<Flower> builder)
    {
        builder.HasKey(flower => flower.Id);

        builder.Property(flower => flower.Name)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(flower => flower.ScientificName)
            .HasColumnType("varchar(50)");

        builder.Property(flower => flower.AppearanceDescription)
            .HasColumnType("varchar(300)")
            .IsRequired();

        builder.Property(flower => flower.CareDescription)
            .HasColumnType("varchar(300)")
            .IsRequired();

        builder.Property(flower => flower.PhotoUrl)
            .HasColumnType("varchar(200)");

        builder.Property(flower => flower.WateringFrequency)
            .IsRequired();

        builder.Property(flower => flower.Soil)
            .IsRequired();

        builder.Property(flower => flower.Size)
            .IsRequired();
        builder.HasCheckConstraint("CK_Size", "\"Size\" >= 0");

        builder.Property(flower => flower.ToxicCategory)
            .IsRequired();

        builder.Property(flower => flower.Illumination)
            .IsRequired();
    }
}