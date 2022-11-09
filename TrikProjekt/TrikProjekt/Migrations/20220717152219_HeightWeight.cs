namespace TrikProjekt56.Migrations
{
    public partial class HeightWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeightId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeightId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Heights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_HeightId",
                table: "Cases",
                column: "HeightId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_WeightId",
                table: "Cases",
                column: "WeightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Heights_HeightId",
                table: "Cases",
                column: "HeightId",
                principalTable: "Heights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Weights_WeightId",
                table: "Cases",
                column: "WeightId",
                principalTable: "Weights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Heights_HeightId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Weights_WeightId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "Heights");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.DropIndex(
                name: "IX_Cases_HeightId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_WeightId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "HeightId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "WeightId",
                table: "Cases");
        }
    }
}
