using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EhsaasHub.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationGiven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonationsGiven",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateGiven = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationsGiven", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationsGiven");
        }
    }
}
