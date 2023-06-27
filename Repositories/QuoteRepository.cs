using gestion_devis.Models;
using Microsoft.EntityFrameworkCore;
using gestion_devis.Data;

namespace gestion_devis.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuoteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Récupère tous les devis avec leurs clients associés
        public List<Quote> GetAllQuotes()
        {
            return _dbContext.Quotes.Include(q => q.Client).ToList();
        }

        // Récupère un devis par son ID avec son client associé
        public Quote GetQuoteById(int id)
        {
            return _dbContext.Quotes.Include(q => q.Client).FirstOrDefault(q => q.Id == id);
        }

        // Ajoute un nouveau devis
        public void AddQuote(Quote quote)
        {
            _dbContext.Quotes.Add(quote);
        }

        // Supprime un devis par son ID
        public void DeleteQuote(int id)
        {
            var quote = _dbContext.Quotes.Find(id);
            if (quote != null)
            {
                _dbContext.Quotes.Remove(quote);
            }
        }

        // Récupère tous les clients avec leurs adresses associées
        public List<Client> GetAllClients()
        {
            return _dbContext.Clients.Include(c => c.Address).ToList();
        }

        // Récupère tous les produits
        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        // Récupère un client par son ID avec son adresse associée
        public Client GetClientById(int id)
        {
            return _dbContext.Clients.Include(c => c.Address).FirstOrDefault(c => c.Id == id);
        }

        // Récupère un produit par son ID
        public Product GetProductById(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        // Ajoute un produit au devis
        public void AddQuoteProduct(QuoteProduct quoteProduct)
        {
            _dbContext.QuoteProducts.Add(quoteProduct);
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
