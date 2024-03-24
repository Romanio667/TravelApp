using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApp.Migrations
{
    /// <inheritdoc />
    public partial class test32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_TripAddress_TripAddressId",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripAddress",
                table: "TripAddress");

            migrationBuilder.RenameTable(
                name: "TripAddress",
                newName: "TripAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripAddresses",
                table: "TripAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_TripAddresses_TripAddressId",
                table: "Trip",
                column: "TripAddressId",
                principalTable: "TripAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_TripAddresses_TripAddressId",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripAddresses",
                table: "TripAddresses");

            migrationBuilder.RenameTable(
                name: "TripAddresses",
                newName: "TripAddress");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripAddress",
                table: "TripAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_TripAddress_TripAddressId",
                table: "Trip",
                column: "TripAddressId",
                principalTable: "TripAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
