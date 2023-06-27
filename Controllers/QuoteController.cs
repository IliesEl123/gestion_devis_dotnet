using gestion_devis.Models;
using gestion_devis.Repositories;
using gestion_devis.ViewModels;
using gestion_devis.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace gestion_devis.Controllers
{
    public class QuoteController : Controller
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly PdfService _pdfService;

        public QuoteController(IQuoteRepository quoteRepository, PdfService pdfService)
        {
            _quoteRepository = quoteRepository;
            _pdfService = pdfService;
        }

        // Affiche la page d'accueil des devis
        public IActionResult Index()
        {
            var quotes = _quoteRepository.GetAllQuotes();
            return View(quotes);
        }

        // Affiche le formulaire de création d'un devis
        public IActionResult Create()
        {
            var viewModel = CreateQuoteViewModel();
            return View(viewModel);
        }

        // Traite la soumission du formulaire de création d'un devis
        [HttpPost]
        public IActionResult Create(CreateQuoteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int selectedClientId = viewModel.SelectedClientId;
                var client = GetClient(selectedClientId);

                if (client != null)
                {
                    var quote = CreateQuoteFromViewModel(viewModel, client);
                    _quoteRepository.AddQuote(quote);
                    _quoteRepository.SaveChanges();

                    var selectedProducts = GetSelectedProductsFromJson(Request.Form["SelectedProducts"]);
                    AddQuoteProducts(quote, selectedProducts);

                    _pdfService.GeneratePdfFromQuote(quote);

                    return RedirectToAction("Index");
                }
            }

            var clients = _quoteRepository.GetAllClients().ToList();
            var products = _quoteRepository.GetAllProducts().ToList();

            viewModel.Clients = clients;
            viewModel.Products = products;

            return View(viewModel);
        }

        // Crée et retourne le ViewModel pour le formulaire de création de devis
        private CreateQuoteViewModel CreateQuoteViewModel()
        {
            var clients = _quoteRepository.GetAllClients().ToList();
            var products = _quoteRepository.GetAllProducts().ToList();

            var viewModel = new CreateQuoteViewModel
            {
                Clients = clients,
                Products = products
            };

            return viewModel;
        }

        // Récupère le client à partir de son ID
        private Client GetClient(int clientId)
        {
            return _quoteRepository.GetClientById(clientId);
        }

        // Crée un objet Quote à partir du ViewModel et du client sélectionné
        private static Quote CreateQuoteFromViewModel(CreateQuoteViewModel viewModel, Client client)
        {
            var quote = new Quote
            {
                Client = client,
                CreationDate = DateTime.Now,
                TotalPrice = viewModel.TotalPrice,
                Discount = viewModel.Discount,
            };

            return quote;
        }

        // Récupère les produits sélectionnés à partir de la chaîne JSON
        private static List<SelectedProductViewModel> GetSelectedProductsFromJson(string selectedProductsJson)
        {
            return JsonSerializer.Deserialize<List<SelectedProductViewModel>>(selectedProductsJson);
        }

        // Ajoute les produits du devis à la base de données
        private void AddQuoteProducts(Quote quote, List<SelectedProductViewModel> selectedProducts)
        {
            foreach (var selectedProduct in selectedProducts)
            {
                var product = _quoteRepository.GetProductById(selectedProduct.ProductId);
                if (product != null)
                {
                    var quoteProduct = new QuoteProduct
                    {
                        QuoteId = quote.Id,
                        ProductId = product.Id,
                        Quantity = selectedProduct.Quantity
                    };

                    _quoteRepository.AddQuoteProduct(quoteProduct);
                }
            }
        }

        [HttpPost]
        public IActionResult DeleteQuote(int id)
        {
            _quoteRepository.DeleteQuote(id);
            _quoteRepository.SaveChanges();

            var quotes = _quoteRepository.GetAllQuotes();
            return PartialView("_QuoteTable", quotes);
        }

        public IActionResult SortQuote(string sortBy)
        {
            IQueryable<Quote> quotesQuery = _quoteRepository.GetAllQuotes().AsQueryable();

            quotesQuery = sortBy switch
            {
                "client" => quotesQuery.OrderBy(q => q.Client.Name),
                "date" => quotesQuery.OrderBy(q => q.CreationDate),
                _ => quotesQuery.OrderBy(q => q.Client.Name),
            };

            var quotes = quotesQuery.ToList();

            return PartialView("_QuoteTable", quotes);
        }

        public IActionResult DownloadPdf(int id)
        {
            var quote = _quoteRepository.GetQuoteById(id);

            if (quote == null || quote.PdfData == null)
            {
                return NotFound(); // Gérer le cas où le devis ou le PDF n'est pas trouvé
            }

            return View(quote);
        }

        public IActionResult GetPdfContent(int id)
        {
            var quote = _quoteRepository.GetQuoteById(id);
            if (quote == null || quote.PdfData == null)
            {
                return NotFound(); // Gérer le cas où le devis ou le PDF n'est pas trouvé
            }

            return File(quote.PdfData, "application/pdf");
        }
    }
}
