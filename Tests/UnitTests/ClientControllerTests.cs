using gestion_devis.Controllers;
using gestion_devis.Data;
using gestion_devis.Models;
using gestion_devis.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace gestion_devis.Tests.UnitTests
{
    public class ClientControllerTests
    {

        [Fact]
        public void CreateClient_WithValidModel_SavesChangesInDatabase()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using var context = new ApplicationDbContext(options);
            {
                ClearDatabase(context);

                var controller = CreateClientController(context);

                var (client, address) = CreateTestClientWithAddress();

                // Act
                var result = controller.Create(client);

                // Assert
                var repository = new ClientRepository(context);
                var clients = repository.GetAllClients().ToList();
                Assert.Single(clients);
                Assert.Equal(client, clients[0]);

                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectResult.ActionName);
            }
        }

        [Fact]
        public void DeleteClient_RemovesClientAndAddressFromDatabase()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions("TestDatabase");

            using var context = new ApplicationDbContext(options);
            {
                ClearDatabase(context);

                var controller = CreateClientController(context);

                var (client, address) = CreateTestClientWithAddress();

                var repository = new ClientRepository(context);
                repository.AddClient(client);
                repository.SaveChanges();

                // Act
                var result = controller.Delete(client.Id, address.Id);

                // Assert
                var deletedClient = repository.GetClientById(client.Id);
                Assert.Null(deletedClient);

                var deletedAddress = repository.GetAddressById(address.Id);
                Assert.Null(deletedAddress);

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

        private static ClientController CreateClientController(ApplicationDbContext context)
        {
            var repository = new ClientRepository(context);
            return new ClientController(repository);
        }

        private static (Client client, Address address) CreateTestClientWithAddress()
        {
            var client = new Client
            {
                Id = 1,
                Name = "Client 1",
                Email = "client1@email.fr",
                PhoneNumber = "1234567890"
            };

            var address = new Address
            {
                Id = 1,
                Street = "Adresse 1",
                City = "Ville 1",
                PostalCode = "12345"
            };

            client.Address = address;

            return (client, address);
        }
    }
}
