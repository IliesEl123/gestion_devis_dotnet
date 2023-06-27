using gestion_devis.Models;

namespace gestion_devis.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int id);
        void AddClient(Client client);      
        void RemoveClient(Client client);
        Address GetAddressById(int id);
        void RemoveAddress(Address address);
        void AddAddress(Address address);
        void SaveChanges();
    }
}
