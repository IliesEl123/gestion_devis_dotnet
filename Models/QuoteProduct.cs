using System.ComponentModel.DataAnnotations;

namespace gestion_devis.Models
{
    public class QuoteProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}
