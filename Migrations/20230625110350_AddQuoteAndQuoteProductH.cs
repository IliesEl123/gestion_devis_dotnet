using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_devis.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteAndQuoteProductH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Quotes",
                newName: "TotalPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Quotes",
                newName: "Price");
        }
    }
}
