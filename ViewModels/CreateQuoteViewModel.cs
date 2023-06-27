using gestion_devis.Models;
using System.Collections.Generic;

namespace gestion_devis.ViewModels
{
    public class CreateQuoteViewModel
    {
        public List<Client> Clients { get; set; }
        public int SelectedClientId { get; set; }
        public List<Product> Products { get; set; }
        public List<SelectedProductViewModel> SelectedProducts { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }

    }

    public class SelectedProductViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
