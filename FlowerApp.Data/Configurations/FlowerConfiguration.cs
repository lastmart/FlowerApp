using FlowerApp.Domain.DbModels;
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
            .HasColumnType("varchar(50)");

        builder.Property(flower => flower.WateringFrequency)
            .IsRequired();
        
        builder.Property(flower => flower.TransplantFrequency)
            .IsRequired();
        
        builder.Property(flower => flower.ToxicCategory)
            .HasColumnType("smallint")
            .IsRequired();
        
        builder.HasOne(flower => flower.LightParameters)
            .WithMany(lightParameters => lightParameters.Flowers)
            .HasForeignKey(flower => flower.LightParametersId)
            .HasPrincipalKey(lightParameters => lightParameters.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}