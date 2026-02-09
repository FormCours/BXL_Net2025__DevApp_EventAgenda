# Event Agenda

## Détail du projet
Backend : `ASP.Net API`  
Frontend : `Client React` (Pour exemple)  
Database : `MS SQL Server`

## Architecture logiciel
Pour cette démo, la `Clean architecture` est mise en place 

### Domain
Les éléments nécessaires du projet.
- Models (+ validation)
- Exceptions
- Enums
- ...

### ApplicationCore
Traitement des `Use cases` du projet.
- Implémentation des règles sous forme de service
- Définition des dépendances en interface

### Presentation
Les projets qui sont utilisables par l'utilisateur.  
Exemple de projet : 
- Web API *
- Application console
- Application Desktop en WPF
- ...

### Infrastructure
Les projets qui permettent d'accéder aux ressources externes.  
Exemple d'accès : 
- Base de données *
- Mail *
- Web API externe
- ...

## Objectif et mise en place de la `Clean architecture` 
Permet de faire de la séparation des préoccupations.  

Exemple d'ordre de définition du code : 
1) Définition des éléments du Domain  
2) Création des services (Interface ? Implémentation)  
3) Couche extérieure  
    - Infrastructure : _Implémentation des besoins des services_  
    - Présentation   : _Utilisation des services_

Exemple de flux de travail (Workflow) : 
- _App client_
    - Requête vers la WebAPI.
- _Présentation_ 
    - Traitement et validation _(DTO)_ de la requête.
    - [Si besoin] Appel aux services pour les règles métiers.
- _ApplicationCore_
    - Traitement des données.
    - [Si besoin] Utilisation des infrastructures.
- _Infrastructure_
    - Interaction avec les ressources externes (DB, Web API, Mail)

### Domain
Noyau du projet !
- Définit les types des objets métiers utilisés (Model, Exception, Enum, ...)
- En appliquant le pattern "Domain Driven Design"
  - Validation des données (Encapsulation forte)
  - Contrôle des opérations de modifications

### ApplicationCore
Application des règles métiers
- Définit l'ensemble des méthodes "service" accessibles _(Interface & Implémentation)_
- Définit les besoins des services via des interfaces _(Infrastructure)_
- Inversion de dépendance ? Les services ne connaissent pas les implémentations

Utilisation du pattern "Facade" et de l'injection de dépendance
- Ne pas manipuler l'implémentation réelle
  - Utilisation de variables typées avec les interfaces 
  - L'implémentation sera fournie via l'injection de dépendance

### Infrastructure
Implémentation des besoins de l'application Core
- Accéder à une source de données _(Base de données, Web API, Fichier, ...)_
- Interagir avec des services externes _(Envoi de mail, ...)_

### Présentation
Mise à disposition des ressources _(Données)_ en appliquant les règles métiers _(ApplicationCore)_
- Web API avec des endpoints adaptés
- Application Web (Exemple : Interface d'administration)
