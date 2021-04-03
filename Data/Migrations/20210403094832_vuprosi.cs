using Microsoft.EntityFrameworkCore.Migrations;

namespace TSP_Uni_Listovki.Data.Migrations
{
    public partial class vuprosi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VuprosiZaListovka",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListovkaID = table.Column<int>(type: "int", nullable: true),
                    VuprosId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VuprosiZaListovka", x => x.id);
                    table.ForeignKey(
                        name: "FK_VuprosiZaListovka_ListovkaModel_ListovkaID",
                        column: x => x.ListovkaID,
                        principalTable: "ListovkaModel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VuprosiZaListovka_VuprosModel_VuprosId",
                        column: x => x.VuprosId,
                        principalTable: "VuprosModel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VuprosiZaListovka_ListovkaID",
                table: "VuprosiZaListovka",
                column: "ListovkaID");

            migrationBuilder.CreateIndex(
                name: "IX_VuprosiZaListovka_VuprosId",
                table: "VuprosiZaListovka",
                column: "VuprosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VuprosiZaListovka");
        }
    }
}
