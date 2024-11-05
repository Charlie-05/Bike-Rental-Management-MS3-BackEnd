using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalApplication.Migrations
{
    /// <inheritdoc />
    public partial class RecordModelFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequest_RentalRecord_RentalRecordId",
                table: "RentalRequest");

            migrationBuilder.DropIndex(
                name: "IX_RentalRequest_RentalRecordId",
                table: "RentalRequest");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_RentalRequestId",
                table: "RentalRecord",
                column: "RentalRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRecord_RentalRequest_RentalRequestId",
                table: "RentalRecord",
                column: "RentalRequestId",
                principalTable: "RentalRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalRecord_RentalRequest_RentalRequestId",
                table: "RentalRecord");

            migrationBuilder.DropIndex(
                name: "IX_RentalRecord_RentalRequestId",
                table: "RentalRecord");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequest_RentalRecordId",
                table: "RentalRequest",
                column: "RentalRecordId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequest_RentalRecord_RentalRecordId",
                table: "RentalRequest",
                column: "RentalRecordId",
                principalTable: "RentalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
