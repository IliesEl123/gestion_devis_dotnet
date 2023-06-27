using gestion_devis.Models;
using gestion_devis.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gestion_devis.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // Affiche la liste des clients
        public IActionResult Index()
        {
            var clients = _clientRepository.GetAllClients();
            return View(clients);
        }

        // Affiche le formulaire de création d'un client
        public IActionResult Create()
        {
            return View();
        }

        // Traite la soumission du formulaire de création d'un client
        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                if (client.Address != null)
                {
                    _clientRepository.AddAddress(client.Address); // Ajouter l'adresse associée
                }

                _clientRepository.AddClient(client); // Ajouter le client à la base de données
                _clientRepository.SaveChanges(); // Enregistrer les modifications dans la base de données

                return RedirectToAction("Index");
            }

            return View(client);
        }

        // Supprime un client et son adresse associée
        [HttpPost]
        public IActionResult Delete(int id, int addressId)
        {
            var client = _clientRepository.GetClientById(id);
            if (client != null)
            {
                var address = _clientRepository.GetAddressById(addressId);
                if (address != null)
                {
                    _clientRepository.RemoveAddress(address); // Supprimer l'adresse associée au client
                }

                _clientRepository.RemoveClient(client); // Supprimer le client
                _clientRepository.SaveChanges(); // Enregistrer les modifications dans la base de données
            }

            return RedirectToAction("Index");
        }
    }
}

