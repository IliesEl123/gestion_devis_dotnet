// Fonction pour mettre à jour les détails du client sur la page
function updateClientDetails() {
    var selectedOption = document.getElementById('clientList').options[document.getElementById('clientList').selectedIndex];
    var clientName = selectedOption.dataset.clientName;
    var clientEmail = selectedOption.dataset.clientEmail;
    var clientPhoneNumber = selectedOption.dataset.clientPhoneNumber;
    var clientStreet = selectedOption.dataset.clientStreet;
    var clientCity = selectedOption.dataset.clientCity;
    var clientPostalCode = selectedOption.dataset.clientPostalCode;

    // Mettre à jour les détails du client sur la page
    document.getElementById('clientName').textContent = 'Nom : ' + clientName;
    document.getElementById('clientEmail').textContent = 'Email : ' + clientEmail;
    document.getElementById('clientPhoneNumber').textContent = 'Numéro de téléphone : ' + clientPhoneNumber;
    document.getElementById('clientAddress').textContent = 'Adresse : ' + clientStreet + ', ' + clientCity + ' ' + clientPostalCode;

    // Mettre à jour la valeur du champ caché pour l'ID du client sélectionné
    document.getElementById('selectedClientId').value = selectedOption.value;

    // Afficher la section "Informations du client"
    document.getElementById('clientDetails').style.display = 'block';
}

// Écouteur d'événement lors de la sélection d'un client
document.getElementById('clientList').addEventListener('change', updateClientDetails);

// Fonction pour ajouter un produit sélectionné au tableau
function addSelectedProductToTable() {
    var selectedOption = document.getElementById('productList').options[document.getElementById('productList').selectedIndex];
    var productId = parseInt(selectedOption.value);
    var productName = selectedOption.text;
    var productDescription = selectedOption.dataset.productDescription;
    var productPrice = parseFloat(selectedOption.dataset.productPrice);
    var quantity = 1; // Valeur par défaut de la quantité

    // Vérifier si le produit est déjà ajouté au tableau
    if (isProductAlreadyAdded(productId)) {
        alert('Ce produit est déjà ajouté au tableau.');
        return;
    }

    // Créer une nouvelle ligne pour le produit
    var row = document.createElement('tr');
    row.id = 'productRow_' + productId;

    // Ajouter les cellules avec les informations du produit
    row.innerHTML =
        '<td>' + productName + '</td>' +
        '<td>' + productDescription + '</td>' +
        '<td>' + productPrice.toFixed(2) + '</td>' +
        '<td><input type="number" id="quantity_' + productId + '" value="' + quantity + '" onchange="updateTotalPrice(' + productId + ', ' + productPrice + ')"></td>' +
        '<td id="totalPrice_' + productId + '">' + productPrice.toFixed(2) + '</td>' +
        '<td><button onclick="removeProductFromTable(' + productId + ')" class="btn btn-danger">Supprimer</button></td>';

    // Ajouter la ligne au tableau
    document.getElementById('selectedProductsTable').getElementsByTagName('tbody')[0].appendChild(row);

    // Réinitialiser la sélection du produit
    document.getElementById('productList').selectedIndex = 0;

    calculateTotalPrice(); // Mettre à jour le prix total du devis
}

// Écouteur d'événement lors de la sélection d'un produit
document.getElementById('productList').addEventListener('change', addSelectedProductToTable);

// Vérifier si un produit est déjà ajouté au tableau
function isProductAlreadyAdded(productId) {
    var existingRow = document.getElementById('productRow_' + productId);
    return (existingRow !== null);
}

// Fonction pour mettre à jour le prix total du produit
function updateTotalPrice(productId, productPrice) {
    var quantity = document.getElementById('quantity_' + productId).value;
    var totalPrice = productPrice * quantity;
    document.getElementById('totalPrice_' + productId).textContent = totalPrice.toFixed(2);

    calculateTotalPrice(); // Mettre à jour le prix total du devis
}

// Fonction pour supprimer un produit du tableau
function removeProductFromTable(productId) {
    var row = document.getElementById('productRow_' + productId);
    row.parentNode.removeChild(row);

    calculateTotalPrice(); // Mettre à jour le prix total du devis
}

// Fonction pour préparer les produits sélectionnés avant la soumission du formulaire
function prepareSelectedProducts() {
    var selectedProducts = [];

    // Parcourir toutes les lignes du tableau
    var tableRows = document.getElementById('selectedProductsTable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');
    for (var i = 0; i < tableRows.length; i++) {
        var row = tableRows[i];
        var productId = parseInt(row.id.split('_')[1]);
        var quantity = parseInt(document.getElementById('quantity_' + productId).value);
        var totalPrice = parseFloat(document.getElementById('totalPrice_' + productId).textContent);

        // Ajouter le produit à la liste des produits sélectionnés
        selectedProducts.push({
            ProductId: productId,
            Quantity: quantity,
            TotalPrice: totalPrice
        });
    }
    formatTotalPrice(); // Appeler la méthode pour formater le champ totalPrice
    formatDiscount(); // Appeler la méthode pour formater le champ discount

    // Convertir la liste des produits sélectionnés en chaîne JSON
    document.getElementById('selectedProducts').value = JSON.stringify(selectedProducts);
}

// Fonction pour calculer le prix total du devis
function calculateTotalPrice() {
    var total = 0;

    // Parcourir toutes les lignes du tableau
    var tableRows = document.getElementById('selectedProductsTable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');
    for (var i = 0; i < tableRows.length; i++) {
        var row = tableRows[i];
        var totalPriceCell = row.getElementsByTagName('td')[4];
        var totalPrice = parseFloat(totalPriceCell.textContent);
        total += totalPrice;
    }

    var discount = parseFloat(document.getElementById('discount').value);
    total -= (total * discount / 100);

    document.getElementById('totalPrice').value = total.toFixed(2).toString();
}

function formatTotalPrice() {
    var totalPriceInput = document.getElementById('totalPrice');
    var totalPriceValue = totalPriceInput.value;

    // Remplacer le point par une virgule dans la valeur
    var formattedValue = totalPriceValue.replace('.', ',');

    // Mettre à jour la valeur du champ avec le format français
    totalPriceInput.value = formattedValue;
}

function formatDiscount() {
    var discountInput = document.getElementById('discount');
    var discountValue = discountInput.value;

    // Remplacer le point par une virgule dans la valeur
    var formattedValue = discountValue.replace('.', ',');

    // Mettre à jour la valeur du champ avec le format français
    discountInput.value = formattedValue;
}
