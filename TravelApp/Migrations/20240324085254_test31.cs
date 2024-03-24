using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApp.Migrations
{
    /// <inheritdoc />
    public partial class test31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Addresses_AddressId",
                table: "Trip");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Trip",
                newName: "TripAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_AddressId",
                table: "Trip",
                newName: "IX_Trip_TripAddressId");

            migrationBuilder.CreateTable(
                name: "TripAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivalCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripAddress", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_TripAddress_TripAddressId",
                table: "Trip",
                column: "TripAddressId",
                principalTable: "TripAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_TripAddress_TripAddressId",
                table: "Trip");

            migrationBuilder.DropTable(
                name: "TripAddress");

            migrationBuilder.RenameColumn(
                name: "TripAddressId",
                table: "Trip",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_TripAddressId",
                table: "Trip",
                newName: "IX_Trip_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Addresses_AddressId",
                table: "Trip",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
