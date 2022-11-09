using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrikProjekt56.Migrations
{
    public partial class CorpseToReligon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Corpses_CorpseId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "Corpses");

            migrationBuilder.RenameColumn(
                name: "CorpseId",
                table: "Cases",
                newName: "ReligionId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_CorpseId",
                table: "Cases",
                newName: "IX_Cases_ReligionId");

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Religions_ReligionId",
                table: "Cases",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Religions_ReligionId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.RenameColumn(
                name: "ReligionId",
                table: "Cases",
                newName: "CorpseId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_ReligionId",
                table: "Cases",
                newName: "IX_Cases_CorpseId");

            migrationBuilder.CreateTable(
                name: "Corpses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corpses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Corpses_CorpseId",
                table: "Cases",
                column: "CorpseId",
                principalTable: "Corpses",
                principalColumn: "Id");
        }
    }
}
