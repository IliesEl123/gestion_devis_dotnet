using gestion_devis.Models;
using gestion_devis.Data;
using Microsoft.EntityFrameworkCore;

namespace gestion_devis.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _dbContext.Clients.Include(c => c.Address).ToList();
        }

        public Client GetClientById(int id)
        {
            return _dbContext.Clients.FirstOrDefault(c => c.Id == id);
        }

        public void AddClient(Client client)
        {
            _dbContext.Clients.Add(client);
            if (client.Address != null)
            {
                _dbContext.Addresses.Add(client.Address);
            }
        }

        public void RemoveClient(Client client)
        {
            _dbContext.Clients.Remove(client);
        }

        public Address GetAddressById(int id)
        {
            return _dbContext.Addresses.FirstOrDefault(c => c.Id == id);
        }

        public void AddAddress(Address address)
        {
            _dbContext.Addresses.Add(address);
        }

        public void RemoveAddress(Address address)
        {
            _dbContext.Addresses.Remove(address);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
