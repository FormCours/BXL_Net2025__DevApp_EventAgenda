# Web API

## Pincipe Restfull
Les requetes d'une API Restfull utilise la méthode de la requete pour définir le type d'action attentu et renvoie un status adapté.

### GET
Récuperation de ressource.  
Réponse attendu : 200, 404.

### POST
Ajouter une nouvelle ressource.  
Réponse attendu : 201, 400, 422.

### PUT
Mise à jour complete d'une ressource.  
Réponse attendu : 204, 400, 404, 422.

### PATCH
Mise à jours partiel d'une ressource.  
Réponse attendu : 204, 400, 404, 422.

### DELETE
Suppression de ressource.  
Réponse attendu : 204, 404.

### HEAD
Vérification de la présence de ressource.  
Réponse attendu : 204, 404.
