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

## Implmentation de l'ASP
On met en place des controllers avec des méthodes qui retourne un `ActionResult` ou `IActionResult`.  
- Le routing est lier aux méthodes des controllers
- Le systeme ASP extrait les données de la requete pour les fournir à la méthode
- Le systeme ASP fourni des méthodes pour générer les réponses

### Extraction des données de la requete
Donnée depuis la route l'url : `exemple.com/product/5` 
```cs
[HttpDelete("{id}")]
public IActionResult Delete([FromRoute] int id)
{
	// ...
}
```

Données depuis les parametres "query" (UrlSearchParams) : `exemple.com/product?page=1&nbElement=10` 
```cs
[HttpGet]
public IActionResult GetAll([FromQuery] int page, [FromQuery] int nbElement)
{
	// ...
}
```

Données depuis le contenu du body (Uniquement : POST, PUT, PATCH)
```cs
[HttpPost]
public IActionResult Add([FromBody] Product data, [FromQuery] apiKey)
{
	// ...
}
```

## Json Web Token (JWT)
Dans une WebAPI Restfull, le server est "stateless" (Ne conserve pas de présistance donnée liée aux requetes - Pas de session).  
Un JWT sert de jeton d’authentification sécurisé qui permet de vérifier l’identité d’un utilisateur sans devoir stocker.

Principe du fonctionnement :
- Le serveur génère un token signé après la connexion de l’utilisateur.
- Le token est envoyé par le client à chaque requête.
- La Web API valide la signature du token et accorder ou refuser l’accès aux routes protégées.

Le framework ASP.Net implément le `JwtSecurityToken ` qui permet de le générer.
