using gestion_devis.Data;
using gestion_devis.Models;

namespace gestion_devis.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _dbContext.Products.Remove(product);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
