using Microsoft.EntityFrameworkCore.Migrations;

namespace TSP_Uni_Listovki.Data.Migrations
{
    public partial class vuprosotgoor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VuprosModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uslovie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tochki = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VuprosModel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OtgovorModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    izobrajenie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    veren = table.Column<bool>(type: "bit", nullable: false),
                    VuprosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtgovorModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_OtgovorModel_VuprosModel_VuprosID",
                        column: x => x.VuprosID,
                        principalTable: "VuprosModel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtgovorModel_VuprosID",
                table: "OtgovorModel",
                column: "VuprosID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtgovorModel");

            migrationBuilder.DropTable(
                name: "VuprosModel");
        }
    }
}
