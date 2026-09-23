using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ledgers.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "uploadedInvoiceUrl",
                table: "Invoice",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uploadedInvoiceUrl",
                table: "Invoice");
        }
    }
}
