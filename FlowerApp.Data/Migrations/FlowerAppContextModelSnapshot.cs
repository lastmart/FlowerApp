﻿// <auto-generated />
using System;
using FlowerApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowerApp.Data.Migrations
{
    [DbContext(typeof(FlowerAppContext))]
    partial class FlowerAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FlowerApp.Data.DbModels.Flowers.Flower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppearanceDescription")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("CareDescription")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<int>("Illumination")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ScientificName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<float>("Size")
                        .HasColumnType("real");

                    b.Property<int>("Soil")
                        .HasColumnType("integer");

                    b.Property<int>("ToxicCategory")
                        .HasColumnType("integer");

                    b.Property<int>("WateringFrequency")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Flowers");

                    b.HasCheckConstraint("CK_Size", "\"Size\" >= 0");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Surveys.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Surveys.SurveyAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("QuestionsMask")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("SurveyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyAnswers");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Surveys.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("QuestionType")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Variants")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Trades.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FlowerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PreferredTrade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("GoogleUserId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("SurveyId")
                        .HasColumnType("integer");

                    b.Property<string>("Telegram")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Surveys.Survey", b =>
                {
                    b.HasOne("FlowerApp.Data.DbModels.Users.User", "User")
                        .WithOne("Survey")
                        .HasForeignKey("FlowerApp.Data.DbModels.Surveys.Survey", "UserId")
                        .HasPrincipalKey("FlowerApp.Data.DbModels.Users.User", "SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Surveys.SurveyAnswer", b =>
                {
                    b.HasOne("FlowerApp.Data.DbModels.Surveys.SurveyQuestion", "SurveyQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FlowerApp.Data.DbModels.Surveys.Survey", "Survey")
                        .WithMany("Answers")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");

                    b.Navigation("SurveyQuestion");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Trades.Trade", b =>
                {
                    b.HasOne("FlowerApp.Data.DbModels.Users.User", "User")
                        .WithMany("Trades")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Surveys.Survey", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Surveys.SurveyQuestion", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("FlowerApp.Data.DbModels.Users.User", b =>
                {
                    b.Navigation("Survey")
                        .IsRequired();

                    b.Navigation("Trades");
                });
#pragma warning restore 612, 618
        }
    }
}
