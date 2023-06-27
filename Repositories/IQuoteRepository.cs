using gestion_devis.Models;

namespace gestion_devis.Repositories
{
    public interface IQuoteRepository
    {
        List<Quote> GetAllQuotes();
        Quote GetQuoteById(int id);
        void AddQuote(Quote quote);
        void DeleteQuote(int id);
        List<Client> GetAllClients();
        List<Product> GetAllProducts();
        Client GetClientById(int id);
        Product GetProductById(int id);
        void AddQuoteProduct(QuoteProduct quoteProduct);
        void SaveChanges();


    }
}
