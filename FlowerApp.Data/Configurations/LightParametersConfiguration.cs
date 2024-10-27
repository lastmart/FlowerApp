using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class LightParametersConfiguration : IEntityTypeConfiguration<LightParameters>
{
    public void Configure(EntityTypeBuilder<LightParameters> builder)
    {
        builder.HasKey(lightParameters => lightParameters.Id);

        builder.Property(lightParameters => lightParameters.DurationInHours)
            .IsRequired();
        builder.HasCheckConstraint("CK_DurationInHours", "\"DurationInHours\" >= 0");

        builder.Property(lightParameters => lightParameters.IlluminationInSuites)
            .HasColumnType("double precision")
            .IsRequired();
        builder.HasCheckConstraint("CK_IlluminationInSuites", "\"IlluminationInSuites\" >= 0");
    }
}