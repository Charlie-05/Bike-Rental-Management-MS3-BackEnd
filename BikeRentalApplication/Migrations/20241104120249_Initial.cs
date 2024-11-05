using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalApplication.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bike",
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
                    table.PrimaryKey("PK_Bike", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    NICNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.NICNumber);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Bike_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bike",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryUnit",
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
                    table.PrimaryKey("PK_InventoryUnit", x => x.RegistrationNo);
                    table.ForeignKey(
                        name: "FK_InventoryUnit_Bike_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bike",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalRecord",
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
                    table.PrimaryKey("PK_RentalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalRecord_InventoryUnit_BikeRegNo",
                        column: x => x.BikeRegNo,
                        principalTable: "InventoryUnit",
                        principalColumn: "RegistrationNo");
                    table.ForeignKey(
                        name: "FK_RentalRecord_User_UserNICNumber",
                        column: x => x.UserNICNumber,
                        principalTable: "User",
                        principalColumn: "NICNumber");
                });

            migrationBuilder.CreateTable(
                name: "RentalRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NICNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RentalRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notify = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalRequest_Bike_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bike",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalRequest_RentalRecord_RentalRecordId",
                        column: x => x.RentalRecordId,
                        principalTable: "RentalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalRequest_User_NICNumber",
                        column: x => x.NICNumber,
                        principalTable: "User",
                        principalColumn: "NICNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_BikeId",
                table: "Image",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryUnit_BikeId",
                table: "InventoryUnit",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_BikeRegNo",
                table: "RentalRecord",
                column: "BikeRegNo");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_UserNICNumber",
                table: "RentalRecord",
                column: "UserNICNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequest_BikeId",
                table: "RentalRequest",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequest_NICNumber",
                table: "RentalRequest",
                column: "NICNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequest_RentalRecordId",
                table: "RentalRequest",
                column: "RentalRecordId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "RentalRequest");

            migrationBuilder.DropTable(
                name: "RentalRecord");

            migrationBuilder.DropTable(
                name: "InventoryUnit");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Bike");
        }
    }
}
