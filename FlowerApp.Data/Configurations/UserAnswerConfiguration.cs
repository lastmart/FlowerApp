using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class UserAnswerConfiguration: IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.UserId)
            .IsRequired();

        builder.Property(ua => ua.QuestionId)
            .IsRequired();

        builder.Property(ua => ua.AnswersSize)
            .IsRequired();

        builder.Property(ua => ua.AnswerMask)
            .IsRequired();
    }
}