# Commande EFCore

## Verification que les outils EFCore sont installé
`dotnet ef`

## Installation des outils
`dotnet tool install --global dotnet-ef`

## Ajouter un migration (Depuis la solution)
Commande à utiliser : 
```
dotnet ef migrations add "<Name>"
```

Commande avec le projet de démarrage et le projet EFCore
```
dotnet ef --project "Demo_WebAPI_EventAgenda.Infrastructure.Database" --startup-project "Demo_WebAPI_EventAgenda.Presentation.WebAPI" migrations add "Initial"
```

### Detail
- La commande `dotnet ef migrations add "<NAME>"` : ajouter une migration
- Option `--project "<Projet>"` : Projet avec le DbContext de EFCore
- Option `--startup-project "<Projet>"` : Projet de démarrage qui doit contenir le tool "Design"

## Appliqué les migrations EFCore sur la DB
Commande à utiliser : 
```
dotnet ef database update
```

Commande avec le projet de démarrage et le projet EFCore
```
dotnet ef --project "Demo_WebAPI_EventAgenda.Infrastructure.Database" --startup-project "Demo_WebAPI_EventAgenda.Presentation.WebAPI" database update
```