using gestion_devis.Models;
using gestion_devis.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gestion_devis.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Affiche la liste des produits
        public IActionResult Index()
        {
            var products = _productRepository.GetAllProducts();
            return View(products);
        }

        // Affiche le formulaire de création d'un produit
        public IActionResult Create()
        {
            return View();
        }

        // Traite la soumission du formulaire de création d'un produit
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.AddProduct(product); // Ajouter le produit à la base de données
                _productRepository.SaveChanges(); // Enregistrer les modifications dans la base de données

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Supprime un produit
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product != null)
            {
                _productRepository.RemoveProduct(product); // Supprimer le produit
                _productRepository.SaveChanges(); // Enregistrer les modifications dans la base de données
            }

            return RedirectToAction("Index");
        }
    }
}
