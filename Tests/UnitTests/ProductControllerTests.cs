using gestion_devis.Controllers;
using gestion_devis.Data;
using gestion_devis.Models;
using gestion_devis.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace gestion_devis.Tests.UnitTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void CreateProduct_WithValidModel_SavesChangesInDatabase()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using var context = new ApplicationDbContext(options);
            {
                ClearDatabase(context);

                var controller = CreateProductController(context);

                var product = CreateTestProduct();

                // Act
                var result = controller.Create(product);

                // Assert
                var repository = new ProductRepository(context);
                var products = repository.GetAllProducts().ToList();
                Assert.Single(products);
                Assert.Equal(product, products[0]);

                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        [Fact]
        public void DeleteProduct_RemovesProductFromDatabase()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using var context = new ApplicationDbContext(options);
            {
                ClearDatabase(context);

                var controller = CreateProductController(context);

                var product = CreateTestProduct();

                var repository = new ProductRepository(context);
                repository.AddProduct(product);
                repository.SaveChanges();

                // Act
                var result = controller.Delete(product.Id);

                // Assert
                var deletedProduct = repository.GetProductById(product.Id);
                Assert.Null(deletedProduct);

                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        private static DbContextOptions<ApplicationDbContext> GetInMemoryDatabaseOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        private static void ClearDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
        }

        private static ProductController CreateProductController(ApplicationDbContext context)
        {
            var repository = new ProductRepository(context);
            return new ProductController(repository);
        }

        private static Product CreateTestProduct()
        {
            return new Product
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                Price = 9.99
            };
        }
    }
}
