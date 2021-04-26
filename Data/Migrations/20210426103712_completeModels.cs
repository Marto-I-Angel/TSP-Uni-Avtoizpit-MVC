using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TSP_Uni_Listovki.Data.Migrations
{
    public partial class completeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstruktoriModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    egn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organisation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstruktoriModel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IzpitvashtiModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    egn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzpitvashtiModel", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "KurmuvaneModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    karani_chasove = table.Column<int>(type: "int", nullable: false),
                    tochki = table.Column<int>(type: "int", nullable: false),
                    instruktorid = table.Column<int>(type: "int", nullable: true),
                    izpitvashtid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KurmuvaneModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_KurmuvaneModel_InstruktoriModel_instruktorid",
                        column: x => x.instruktorid,
                        principalTable: "InstruktoriModel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KurmuvaneModel_IzpitvashtiModel_izpitvashtid",
                        column: x => x.izpitvashtid,
                        principalTable: "IzpitvashtiModel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VuprosModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uslovie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tochki = table.Column<int>(type: "int", nullable: false),
                    ListovkaModelid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VuprosModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_VuprosModel_ListovkaModel_ListovkaModelid",
                        column: x => x.ListovkaModelid,
                        principalTable: "ListovkaModel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_KurmuvaneModel_instruktorid",
                table: "KurmuvaneModel",
                column: "instruktorid");

            migrationBuilder.CreateIndex(
                name: "IX_KurmuvaneModel_izpitvashtid",
                table: "KurmuvaneModel",
                column: "izpitvashtid");

            migrationBuilder.CreateIndex(
                name: "IX_OtgovorModel_VuprosID",
                table: "OtgovorModel",
                column: "VuprosID");

            migrationBuilder.CreateIndex(
                name: "IX_VuprosiZaListovka_ListovkaID",
                table: "VuprosiZaListovka",
                column: "ListovkaID");

            migrationBuilder.CreateIndex(
                name: "IX_VuprosiZaListovka_VuprosId",
                table: "VuprosiZaListovka",
                column: "VuprosId");

            migrationBuilder.CreateIndex(
                name: "IX_VuprosModel_ListovkaModelid",
                table: "VuprosModel",
                column: "ListovkaModelid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KurmuvaneModel");

            migrationBuilder.DropTable(
                name: "OtgovorModel");

            migrationBuilder.DropTable(
                name: "VuprosiZaListovka");

            migrationBuilder.DropTable(
                name: "InstruktoriModel");

            migrationBuilder.DropTable(
                name: "IzpitvashtiModel");

            migrationBuilder.DropTable(
                name: "VuprosModel");

            migrationBuilder.DropTable(
                name: "ListovkaModel");
        }
    }
}
