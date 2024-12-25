using FlowerApp.Data.DbModels.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class SurveyAnswerConfiguration: IEntityTypeConfiguration<SurveyAnswer>
{
    public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
    {
        builder.HasKey(answer => answer.Id);
        
        builder.Property(answer => answer.QuestionsMask)
            .HasMaxLength(30)
            .IsRequired();
        
        // TODO add constraints

        builder
            .HasOne(answer => answer.SurveyQuestion)
            .WithMany(question => question.Answers)
            .HasForeignKey(answer => answer.QuestionId)
            .HasPrincipalKey(question => question.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}