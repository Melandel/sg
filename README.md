#  Kata « Réservations »

 On souhaite exposer une API C# pour réserver des salles de réunion.

## Enoncé
### Fonctionnellement, on souhaite
- Lister des salles
- Créer des réservations
- Supprimer des réservations
- En cas de conflit (de réservation), proposer tous les créneaux libres de la journée demandée.
- Si possible dans le temps imparti, l'API doit exposer un Swagger/Swagger UI et être utilisable ( activer le "Try-Out" de Swagger UI)

### Pour simplifier au maximum l’exercice
1) il y a 10 salles ( « room0 » … « room9 » )
2) une réservation est
	- Au nom d’une seule personne
	- Concerne toujours une seule salle
	- Sur un créneau début / fin - la journée est découpée en créneaux d’1 heure (24 créneaux donc)
	- Jamais sur plusieurs jours
3) le back-end doit être in-memory – (pas de dépendance a une base). Mais fonctionnellement, vous devez gérer les conflits (donc maintenir un état) – cf ci-dessus

### Le but de ce kata est de montrer
- Votre maîtrise de .NET core Web API
- Votre approche de modélisation « restful » de l'API (url paths, status codes …)

N’investissez pas plus de 0.5j sur le sujet, il s’agit d’un kata de quelques heures

Le résultat est à mettre sur un repo github.com accessible, que nous puissions récupérer

Le résultat doit pouvoir compiler ( aka ne pas tirer des dépendances privées ou sous licence )
