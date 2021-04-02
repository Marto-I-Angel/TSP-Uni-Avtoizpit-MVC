using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TSP_Uni_Listovki.Data.Migrations
{
    public partial class userAndListovka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListovkaID",
                table: "VuprosModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListovkaModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tochki = table.Column<int>(type: "int", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListovkaModel", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VuprosModel_ListovkaID",
                table: "VuprosModel",
                column: "ListovkaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VuprosModel_ListovkaModel_ListovkaID",
                table: "VuprosModel",
                column: "ListovkaID",
                principalTable: "ListovkaModel",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VuprosModel_ListovkaModel_ListovkaID",
                table: "VuprosModel");

            migrationBuilder.DropTable(
                name: "ListovkaModel");

            migrationBuilder.DropIndex(
                name: "IX_VuprosModel_ListovkaID",
                table: "VuprosModel");

            migrationBuilder.DropColumn(
                name: "ListovkaID",
                table: "VuprosModel");
        }
    }
}
