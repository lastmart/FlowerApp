using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowerApp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LightParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IlluminationInSuites = table.Column<double>(type: "double precision", nullable: false),
                    DurationInHours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    ScientificName = table.Column<string>(type: "varchar(50)", nullable: false),
                    AppearanceDescription = table.Column<string>(type: "varchar(300)", nullable: false),
                    CareDescription = table.Column<string>(type: "varchar(300)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "varchar(50)", nullable: false),
                    WateringFrequency = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TransplantFrequency = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LightParametersId = table.Column<int>(type: "integer", nullable: false),
                    ToxicCategory = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flowers_LightParameters_LightParametersId",
                        column: x => x.LightParametersId,
                        principalTable: "LightParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_LightParametersId",
                table: "Flowers",
                column: "LightParametersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flowers");

            migrationBuilder.DropTable(
                name: "LightParameters");
        }
    }
}
