using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalApplication.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    NICNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.NICNumber);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryUnits",
                columns: table => new
                {
                    RegistrationNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YearOfManufacture = table.Column<int>(type: "int", nullable: true),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryUnits", x => x.RegistrationNo);
                    table.ForeignKey(
                        name: "FK_InventoryUnits_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notify = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalRequests_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "NICNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RentalOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RentalReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BikeRegNo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RentalRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserNICNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalRecords_InventoryUnits_BikeRegNo",
                        column: x => x.BikeRegNo,
                        principalTable: "InventoryUnits",
                        principalColumn: "RegistrationNo");
                    table.ForeignKey(
                        name: "FK_RentalRecords_RentalRequests_RentalRequestId",
                        column: x => x.RentalRequestId,
                        principalTable: "RentalRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalRecords_Users_UserNICNumber",
                        column: x => x.UserNICNumber,
                        principalTable: "Users",
                        principalColumn: "NICNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_BikeId",
                table: "Images",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryUnits_BikeId",
                table: "InventoryUnits",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecords_BikeRegNo",
                table: "RentalRecords",
                column: "BikeRegNo");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecords_RentalRequestId",
                table: "RentalRecords",
                column: "RentalRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecords_UserNICNumber",
                table: "RentalRecords",
                column: "UserNICNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequests_BikeId",
                table: "RentalRequests",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequests_UserId",
                table: "RentalRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "RentalRecords");

            migrationBuilder.DropTable(
                name: "InventoryUnits");

            migrationBuilder.DropTable(
                name: "RentalRequests");

            migrationBuilder.DropTable(
                name: "Bikes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
