# Event Agenda

## Détail du projet
Backend : `ASP.Net API`  
Frontend : `Client React` (Pour exemple)
Database : `MS SQL Server`

## Architecture logiciel : `Clean architecture`  

### Domain
Les éléments necessaire du projet.
- Models (+ validation)
- Exceptions
- Enums
- ...

### ApplicationCore
Traitement des `Use cases` du projet.
- Implementation des règles sous forme de service
- Définition des dépendances en interface

### Presentation
Les projets qui sont utilisable par l'utilisateur.  
Exemple de projet : 
- Web API *
- Application console
- Application Desktop en WPF
- ...

### Infrastructure
Les projets qui permette d'acceder au ressource externe.
Exemple d'acces : 
- Base de donnée *
- Mail *
- Web API externe
- ...