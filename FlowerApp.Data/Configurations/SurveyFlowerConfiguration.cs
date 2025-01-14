using FlowerApp.Data.DbModels.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class SurveyFlowerConfiguration : IEntityTypeConfiguration<SurveyFlower>
{
    public void Configure(EntityTypeBuilder<SurveyFlower> builder)
    {
        builder.HasKey(flower => flower.Id);

        builder
            .Property(flower => flower.RelevantVariantsProbabilities)
            .IsRequired();

        builder
            .HasOne(flower => flower.SurveyQuestion)
            .WithMany(question => question.SurveyFlowers)
            .HasForeignKey(flower => flower.SurveyQuestionId)
            .HasPrincipalKey(question => question.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(surveyFlower => surveyFlower.Flower)
            .WithMany(flower => flower.SurveyFlowers)
            .HasForeignKey(surveyFlower => surveyFlower.FlowerId)
            .HasPrincipalKey(flower => flower.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}