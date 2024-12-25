using FlowerApp.Data.DbModels.AuthTokens;
using FlowerApp.Data.DbModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowerApp.Data.Configurations;

public class AuthTokensConfiguration : IEntityTypeConfiguration<AuthTokens>
{
    public void Configure(EntityTypeBuilder<AuthTokens> builder)
    {
        builder.HasKey(tokens => tokens.Id);

        builder.Property(tokens => tokens.AccessToken)
            .IsRequired();

        builder
            .HasOne(tokens => tokens.User)
            .WithOne(user => user.Tokens)
            .HasForeignKey<AuthTokens>(tokens => tokens.UserId)
            .HasPrincipalKey<User>(user => user.AuthTokensId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}