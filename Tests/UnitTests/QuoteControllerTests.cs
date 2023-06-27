
//using gestion_devis.Data;
//using gestion_devis.Models;
//using Microsoft.EntityFrameworkCore;
//using Xunit;

//namespace gestion_devis.Tests.UnitTests
//{
//    public class QuoteControllerTests
//    {

//        private static DbContextOptions<ApplicationDbContext> GetInMemoryDatabaseOptions(string databaseName)
//        {
//            return new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName)
//                .Options;
//        }

//        private static void ClearDatabase(ApplicationDbContext context)
//        {
//            context.Database.EnsureDeleted();
//        }

//        private static Quote CreateTestQuote()
//        {
//            var client = new Client
//            {
//                Id = 1,
//                Name = "Client 1",
//                Address = new Address
//                {
//                    Street = "Rue Client 1",
//                    PostalCode = "12345",
//                    City = "Ville Client 1"
//                }
//            };

//            var product = new Product
//            {
//                Id = 1,
//                Name = "Produit 1",
//                Description = "Description 1",
//                Price = 9.99
//            };

//            var quoteProduct = new QuoteProduct
//            {
//                Id = 1,
//                Quantity = 1,
//                ProductId = product.Id,
//                Product = product
//            };


//            var quote = new Quote
//            {
//                Id = 1,
//                Client = client,
//                CreationDate = DateTime.Now,
//                Discount = 10.0,
//                TotalPrice = 99.90,
//                QuoteProducts = new List<QuoteProduct> { quoteProduct },
//                PdfData = null
//            };

//            return quote;
//        }
//    }
//}
