namespace TrikProjekt56.Migrations
{
    public partial class ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgeId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorpseId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistFeatureId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HairId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_AgeId",
                table: "Cases",
                column: "AgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CorpseId",
                table: "Cases",
                column: "CorpseId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_DistFeatureId",
                table: "Cases",
                column: "DistFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_EducationId",
                table: "Cases",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_HairId",
                table: "Cases",
                column: "HairId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_LocationId",
                table: "Cases",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Ages_AgeId",
                table: "Cases",
                column: "AgeId",
                principalTable: "Ages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Corpses_CorpseId",
                table: "Cases",
                column: "CorpseId",
                principalTable: "Corpses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_DistFeatures_DistFeatureId",
                table: "Cases",
                column: "DistFeatureId",
                principalTable: "DistFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Educations_EducationId",
                table: "Cases",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Hairs_HairId",
                table: "Cases",
                column: "HairId",
                principalTable: "Hairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Locations_LocationId",
                table: "Cases",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Ages_AgeId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Corpses_CorpseId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_DistFeatures_DistFeatureId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Educations_EducationId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Hairs_HairId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Locations_LocationId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_AgeId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CorpseId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_DistFeatureId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_EducationId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_HairId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_LocationId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "AgeId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CorpseId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "DistFeatureId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "HairId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Cases");
        }
    }
}
