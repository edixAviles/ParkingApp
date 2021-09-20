using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KrugerApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MCustomer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MVehicleType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValuePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MVehicleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MVehicle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MVehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MVehicle_MCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "MCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MVehicle_MVehicleType_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "MVehicleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MParking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParkingValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MParking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MParking_MVehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "MVehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MParking_VehicleId",
                table: "MParking",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_MVehicle_CustomerId",
                table: "MVehicle",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MVehicle_VehicleTypeId",
                table: "MVehicle",
                column: "VehicleTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MParking");

            migrationBuilder.DropTable(
                name: "MVehicle");

            migrationBuilder.DropTable(
                name: "MCustomer");

            migrationBuilder.DropTable(
                name: "MVehicleType");
        }
    }
}
