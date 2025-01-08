using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowerApp.Data.Migrations
{
    public partial class UpdateSurveyFlowers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Users_UserId",
                table: "Surveys");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_SurveyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys");

            migrationBuilder.AddColumn<string>(
                name: "GoogleUserId",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Questions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "SurveyFlowerId",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SurveyFlowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RelevantVariantsProbabilities = table.Column<string>(type: "text", nullable: false),
                    FlowerId = table.Column<int>(type: "integer", nullable: false),
                    SurveyQuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyFlowers", x => x.Id);
                    table.UniqueConstraint("AK_SurveyFlowers_SurveyQuestionId", x => x.SurveyQuestionId);
                    table.ForeignKey(
                        name: "FK_SurveyFlowers_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SurveyId",
                table: "Users",
                column: "SurveyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveyFlowers_FlowerId",
                table: "SurveyFlowers",
                column: "FlowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_SurveyFlowers_Id",
                table: "Questions",
                column: "Id",
                principalTable: "SurveyFlowers",
                principalColumn: "SurveyQuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Surveys_SurveyId",
                table: "Users",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_SurveyFlowers_Id",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Surveys_SurveyId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SurveyFlowers");

            migrationBuilder.DropIndex(
                name: "IX_Users_SurveyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GoogleUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SurveyFlowerId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Questions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_SurveyId",
                table: "Users",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_UserId",
                table: "Surveys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "SurveyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
