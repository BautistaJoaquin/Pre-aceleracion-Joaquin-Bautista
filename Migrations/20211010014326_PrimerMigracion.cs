using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace preAceleracionDisney.Migrations
{
    public partial class PrimerMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "disney");

            migrationBuilder.CreateTable(
                name: "Characters",
                schema: "disney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                schema: "disney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "disney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Qualification = table.Column<int>(type: "int", nullable: false),
                    gendersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genders_gendersId",
                        column: x => x.gendersId,
                        principalSchema: "disney",
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                schema: "disney",
                columns: table => new
                {
                    charactersId = table.Column<int>(type: "int", nullable: false),
                    moviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.charactersId, x.moviesId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Characters_charactersId",
                        column: x => x.charactersId,
                        principalSchema: "disney",
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movies_moviesId",
                        column: x => x.moviesId,
                        principalSchema: "disney",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_moviesId",
                schema: "disney",
                table: "CharacterMovie",
                column: "moviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_gendersId",
                schema: "disney",
                table: "Movies",
                column: "gendersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie",
                schema: "disney");

            migrationBuilder.DropTable(
                name: "Characters",
                schema: "disney");

            migrationBuilder.DropTable(
                name: "Movies",
                schema: "disney");

            migrationBuilder.DropTable(
                name: "Genders",
                schema: "disney");
        }
    }
}
