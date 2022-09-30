using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrikProjekt56.Migrations
{
    public partial class FeatureToGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_DistFeatures_DistFeatureId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Locations_LocationId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "DistFeatures");

            migrationBuilder.RenameColumn(
                name: "DistFeatureId",
                table: "Cases",
                newName: "GenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_DistFeatureId",
                table: "Cases",
                newName: "IX_Cases_GenderId");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Genders_GenderId",
                table: "Cases",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Locations_LocationId",
                table: "Cases",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Genders_GenderId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Locations_LocationId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Cases",
                newName: "DistFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_GenderId",
                table: "Cases",
                newName: "IX_Cases_DistFeatureId");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "DistFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistFeatures", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_DistFeatures_DistFeatureId",
                table: "Cases",
                column: "DistFeatureId",
                principalTable: "DistFeatures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Locations_LocationId",
                table: "Cases",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
