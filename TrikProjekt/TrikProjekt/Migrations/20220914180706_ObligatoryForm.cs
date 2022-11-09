using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrikProjekt56.Migrations
{
    public partial class ObligatoryForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Ages_AgeId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Educations_EducationId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Genders_GenderId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Hairs_HairId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Heights_HeightId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Religions_ReligionId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Weights_WeightId",
                table: "Cases");

            migrationBuilder.AlterColumn<int>(
                name: "WeightId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReligionId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HeightId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HairId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgeId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Ages_AgeId",
                table: "Cases",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Educations_EducationId",
                table: "Cases",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Genders_GenderId",
                table: "Cases",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Hairs_HairId",
                table: "Cases",
                column: "HairId",
                principalTable: "Hairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Heights_HeightId",
                table: "Cases",
                column: "HeightId",
                principalTable: "Heights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Religions_ReligionId",
                table: "Cases",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Weights_WeightId",
                table: "Cases",
                column: "WeightId",
                principalTable: "Weights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Ages_AgeId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Educations_EducationId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Genders_GenderId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Hairs_HairId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Heights_HeightId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Religions_ReligionId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Weights_WeightId",
                table: "Cases");

            migrationBuilder.AlterColumn<int>(
                name: "WeightId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReligionId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HeightId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HairId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgeId",
                table: "Cases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Ages_AgeId",
                table: "Cases",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Educations_EducationId",
                table: "Cases",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Genders_GenderId",
                table: "Cases",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Hairs_HairId",
                table: "Cases",
                column: "HairId",
                principalTable: "Hairs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Heights_HeightId",
                table: "Cases",
                column: "HeightId",
                principalTable: "Heights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Religions_ReligionId",
                table: "Cases",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Weights_WeightId",
                table: "Cases",
                column: "WeightId",
                principalTable: "Weights",
                principalColumn: "Id");
        }
    }
}
