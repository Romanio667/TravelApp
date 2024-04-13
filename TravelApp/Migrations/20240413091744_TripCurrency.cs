using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApp.Migrations
{
    /// <inheritdoc />
    public partial class TripCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Trip");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyCategory",
                table: "Trip",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCategory",
                table: "Trip");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
