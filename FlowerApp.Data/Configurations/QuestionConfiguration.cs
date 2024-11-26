using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);
        
        builder.Property(q => q.QuestionText)
            .IsRequired()
            .HasColumnType("varchar(300)");

        builder.Property(q => q.AnswerSize)
            .IsRequired();

        builder.Property(q => q.AnswerOptions)
            .IsRequired()
            .HasColumnType("text[]");
    }
}