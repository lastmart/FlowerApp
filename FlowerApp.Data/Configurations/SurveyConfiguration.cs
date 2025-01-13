using FlowerApp.Data.DbModels.Surveys;
using FlowerApp.Data.DbModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.HasKey(survey => survey.Id);

        builder
            .HasMany(survey => survey.Answers)
            .WithOne(answer => answer.Survey)
            .HasForeignKey(answer => answer.SurveyId)
            .HasPrincipalKey(survey => survey.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // builder
            // .HasOne(survey => survey.User)
            // .WithOne(user => user.Survey)
            // .HasForeignKey<User>(user => user.SurveyId)
            // .HasPrincipalKey<Survey>(survey => survey.Id)
            // .OnDelete(DeleteBehavior.Cascade);
    }
}