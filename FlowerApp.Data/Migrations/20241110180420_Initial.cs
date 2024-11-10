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
                    Size = table.Column<float>(type: "real", nullable: false),
                    WateringFrequency = table.Column<int>(type: "integer", nullable: false),
                    Soil = table.Column<int>(type: "integer", nullable: false),
                    ToxicCategory = table.Column<int>(type: "integer", nullable: false),
                    Illumination = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.Id);
                    table.CheckConstraint("CK_Size", "\"Size\" >= 0");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flowers");
        }
    }
}
