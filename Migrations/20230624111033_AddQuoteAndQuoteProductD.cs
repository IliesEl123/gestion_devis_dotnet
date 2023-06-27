using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_devis.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteAndQuoteProductD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteProduct_Products_ProductId",
                table: "QuoteProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteProduct_Quotes_QuoteId",
                table: "QuoteProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteProduct",
                table: "QuoteProduct");

            migrationBuilder.RenameTable(
                name: "QuoteProduct",
                newName: "QuoteProducts");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteProduct_QuoteId",
                table: "QuoteProducts",
                newName: "IX_QuoteProducts_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteProduct_ProductId",
                table: "QuoteProducts",
                newName: "IX_QuoteProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteProducts",
                table: "QuoteProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteProducts_Products_ProductId",
                table: "QuoteProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteProducts_Quotes_QuoteId",
                table: "QuoteProducts",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteProducts_Products_ProductId",
                table: "QuoteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteProducts_Quotes_QuoteId",
                table: "QuoteProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteProducts",
                table: "QuoteProducts");

            migrationBuilder.RenameTable(
                name: "QuoteProducts",
                newName: "QuoteProduct");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteProducts_QuoteId",
                table: "QuoteProduct",
                newName: "IX_QuoteProduct_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteProducts_ProductId",
                table: "QuoteProduct",
                newName: "IX_QuoteProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteProduct",
                table: "QuoteProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteProduct_Products_ProductId",
                table: "QuoteProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteProduct_Quotes_QuoteId",
                table: "QuoteProduct",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
