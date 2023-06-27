using gestion_devis.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Layout.Properties;
using gestion_devis.Data;

namespace gestion_devis.Utils
{
    public class PdfService
    {
        private readonly ApplicationDbContext _dbContext;

        public PdfService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GeneratePdfFromQuote(Quote quote)
        {

            var memoryStream = new MemoryStream();
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // En-tête du devis
                var header = new Paragraph("Devis")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(20)
                    .SetTextAlignment(TextAlignment.CENTER);
                document.Add(header);

                document.Add(new Paragraph("Date du devis : " + quote.CreationDate.ToString("dd/MM/yyyy"))
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(12));
                document.Add(new Paragraph("Client : " +quote.Client.Name )
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(12));
                document.Add(new Paragraph("Adresse : " + quote.Client.Address.Street + " " + quote.Client.Address.PostalCode+ " " + quote.Client.Address.City)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetFontSize(12));
                document.Add(new Paragraph().SetMarginBottom(20));

                // Tableau des produits
                var table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
                table.AddHeaderCell(new Cell().Add(new Paragraph("Produit")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(10)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Quantité")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(10)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Prix unitaire")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(10)));
                table.AddHeaderCell(new Cell().Add(new Paragraph("Total")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(10)));

                double totalAmount = 0;

                foreach (var quoteProduct in quote.QuoteProducts)
                {
                    var product = quoteProduct.Product;
                    double productTotal = quoteProduct.Quantity * product.Price;

                    table.AddCell(new Cell().Add(new Paragraph(product.Name)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                        .SetFontSize(10)));
                    table.AddCell(new Cell().Add(new Paragraph(quoteProduct.Quantity.ToString())
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                        .SetFontSize(10)));
                    table.AddCell(new Cell().Add(new Paragraph(product.Price.ToString("C"))
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                        .SetFontSize(10)));
                    table.AddCell(new Cell().Add(new Paragraph(productTotal.ToString("C"))
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                        .SetFontSize(10)));

                    totalAmount += productTotal;
                }

                // Total sans remise du devis
                document.Add(table);
                document.Add(new Paragraph().SetMarginTop(20));
                document.Add(new Paragraph("Montant total sans remise : " + totalAmount.ToString("C"))
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(14));
                // Remise accordée au devis
                document.Add(new Paragraph().SetMarginTop(10));
                document.Add(new Paragraph("Remise : " + quote.Discount + " %")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(14));
                // Total avec remise du devis
                document.Add(new Paragraph().SetMarginTop(10));
                document.Add(new Paragraph("Montant total avec remise : " + quote.TotalPrice)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(14));

                document.Close();

                // Enregistrer les données du PDF dans la propriété PdfData de l'objet Quote
                quote.PdfData = memoryStream.ToArray();

                // Enregistrer les modifications dans la base de données
                _dbContext.SaveChanges();
            }
        
    }
}
