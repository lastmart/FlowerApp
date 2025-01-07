using FlowerApp.Data.DbModels.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class SurveyFlowerConfiguration : IEntityTypeConfiguration<SurveyFlower>
{
    public void Configure(EntityTypeBuilder<SurveyFlower> builder)
    {
        builder.HasKey(flower => flower.Id);

        builder.Property(flower => flower.RelevantVariants)
            .IsRequired();

        // TODO add constraints
    }
}