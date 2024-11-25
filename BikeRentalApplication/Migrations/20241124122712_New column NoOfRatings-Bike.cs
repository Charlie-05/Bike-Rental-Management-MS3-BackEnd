using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalApplication.Migrations
{
    /// <inheritdoc />
    public partial class NewcolumnNoOfRatingsBike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRatings",
                table: "Bikes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRatings",
                table: "Bikes");
        }
    }
}
