
# Gestion de Devis

Ce projet vise à développer une application de gestion de devis pour une entreprise.

## Fonctionnalités

- **Création de devis** : l'application permet de créer de nouveaux devis en spécifiant les détails du client, les produits et les quantités.
- **Affichage des devis** : les devis créés peuvent être affichés dans une liste, montrant les informations clés telles que le nom du client, la date de création et un lien pour télécharger le devis au format PDF.
- **Tri des devis** : les devis peuvent être triés par date ou par client.
- **Gestion des clients** : l'application permet d'ajouter, afficher et supprimer des clients, en enregistrant leurs informations telles que le nom, l'e-mail, le numéro de téléphone et l'adresse.
- **Gestion des produits** : les produits associés aux devis peuvent être ajoutés, affichés et supprimés, en spécifiant leur nom, leur description et leur prix.

## Tâches à réaliser avant l'ajout de nouvelles fonctionnalités

- Stratégie de tests
- Gestion des exceptions
- Validation des données
- Optimisation des performances
- Améliorations de l'expérience utilisateur (front)
- Documentation

## Technologies utilisées

- **.NET (version 6.0)** : la version du framework .NET utilisée dans ce projet.
- **ASP.NET Core (version 7.0.304)** : le projet est développé en utilisant le framework ASP.NET Core.
- **HTML/CSS** : les pages sont stylisées en utilisant des langages standard du web.
- **JavaScript** : des fonctionnalités interactives sont implémentées à l'aide de JavaScript.
- **Entity Framework Core** : pour la gestion des données, Entity Framework Core est utilisé comme ORM (Object-Relational Mapping).
- **Microsoft SQL Server** : pour la persistance des données, Microsoft SQL Server est utilisé comme système de gestion de base de données relationnelle.

## Packages utilisés

- **itext7 (version 8.0.0)** : Utilisé pour générer des documents PDF.
- **Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation (version 6.0.3)** : Permet la compilation Razor en temps réel.
- **Microsoft.EntityFrameworkCore (version 7.0.8)** : Utilisé pour la gestion de la base de données.
- **Microsoft.EntityFrameworkCore.SqlServer (version 7.0.8)** : Pilote SQL Server pour Entity Framework Core.
- **Microsoft.EntityFrameworkCore.Tools (version 7.0.8)** : Outils pour Entity Framework Core.
- **Microsoft.EntityFrameworkCore.InMemory (version 7.0.8)** : Utilisé pour les tests avec une base de données en mémoire.
- **Moq (version 4.18.4)** : Utilisé pour les tests unitaires.
- **xunit (version 2.4.2)** : Framework de tests unitaires.
- **xunit.runner.visualstudio (version 2.4.5)** : Utilisé pour l'exécution des tests avec Visual Studio.
