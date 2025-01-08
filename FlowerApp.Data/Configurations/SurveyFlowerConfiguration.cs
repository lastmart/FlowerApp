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
            .WithOne(question => question.SurveyFlower)
            .HasForeignKey<SurveyQuestion>(question => question.SurveyFlowerId)
            .HasPrincipalKey<SurveyFlower>(flower => flower.Id)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(surveyFlower => surveyFlower.Flower)
            .WithMany(flower => flower.SurveyFlowers)
            .HasForeignKey(surveyFlower => surveyFlower.FlowerId)
            .HasPrincipalKey(flower => flower.Id)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(flower => flower.SurveyQuestion)
            .WithOne(question => question.SurveyFlower)
            .HasForeignKey<SurveyQuestion>(question => question.Id)
            .HasPrincipalKey<SurveyFlower>(flower => flower.SurveyQuestionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}