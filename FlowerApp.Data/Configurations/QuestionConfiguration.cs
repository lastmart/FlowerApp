using FlowerApp.Data.DbModels.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
{
    public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
    {
        builder.HasKey(survey => survey.Id);

        builder
            .Property(question => question.QuestionType)
            .IsRequired();

        builder
            .Property(question => question.Variants)
            .IsRequired();

        builder
            .Property(question => question.Text)
            .IsRequired()
            .HasColumnType("varchar(300)");

        // TODO add constraints
    }
}