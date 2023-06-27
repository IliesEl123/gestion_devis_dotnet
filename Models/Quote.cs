using System.ComponentModel.DataAnnotations;

namespace gestion_devis.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Client Client { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

       
        [Range(0, 100)]
        public double Discount { get; set; }

       
        [Range(0, double.MaxValue)]
        public double TotalPrice { get; set; }

        public List<QuoteProduct> QuoteProducts { get; set; }

        public byte[] PdfData { get; set; }

        public Quote()
        {
            QuoteProducts = new List<QuoteProduct>();
        }
    }
}
