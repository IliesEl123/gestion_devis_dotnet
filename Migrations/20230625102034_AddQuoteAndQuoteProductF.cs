using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_devis.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteAndQuoteProductF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Quotes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Quotes");
        }
    }
}
