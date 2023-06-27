using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_devis.Migrations
{
    /// <inheritdoc />
    public partial class AddPdfDataToQuote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PdfData",
                table: "Quotes",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfData",
                table: "Quotes");
        }
    }
}
