using gestion_devis.Models;

namespace gestion_devis.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void RemoveProduct(Product product);
        void SaveChanges();
    }
}
