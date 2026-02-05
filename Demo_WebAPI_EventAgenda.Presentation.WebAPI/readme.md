# Web API

## Principe Restfull
Les requêtes d'une API Restfull utilise la méthode de la requete pour définir le type d'action attendue et renvoie un status adapté.

### GET
Récuperation de ressource(s).  
Réponse attendue : 200, 404.

### POST
Ajouter une nouvelle ressource.  
Réponse attendue : 201, 400, 422.

### PUT
Mise à jour complete d'une ressource.  
Réponse attendue : 204, 400, 404, 422.

### PATCH
Mise à jours partielle d'une ressource.  
Réponse attendue : 204, 400, 404, 422.

### DELETE
Suppression de ressource(s).  
Réponse attendue : 204, 404.

### HEAD
Vérification de la présence de ressource(s).  
Réponse attendue : 204, 404.
