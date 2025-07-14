using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EhsaasHub.Migrations
{
    /// <inheritdoc />
    public partial class InitDonationReceived : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonationsReceived",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationsReceived", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationsReceived");
        }
    }
}
