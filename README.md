# 🎯 Gestion des Compétences - API Backend

API RESTful développée avec **ASP.NET Core (.NET 10)** permettant aux utilisateurs de gérer, suivre et valoriser leurs compétences techniques et professionnelles, leurs niveaux d'expertise et leurs certifications obtenues.

---

## 🏛️ Architecture & Conception

Le projet est conçu selon les principes de la **Clean Architecture** (architecture en couches) et sépare distinctement les responsabilités :

```
GestionCompetences/
├── GestionCompetences.Domain/          # Cœur métier : Entités, Enums
├── GestionCompetences.Application/     # Cas d'usage : Commandes / Requêtes (CQRS), DTOs, Interfaces, Validations
├── GestionCompetences.Infrastructure/  # Persistance : EF Core, SQL Server, Auth, Services
└── GestionCompetences.API/             # Présentation : Contrôleurs REST, Auth JWT, OpenAPI / Scalar
```

### 🛠️ Patterns & Bonnes Pratiques
- **CQRS (Command & Query Separation)** : Séparation stricte entre les commandes de mutation (`ICommandDefinition`, `ICommandeHandler`) et les requêtes de lecture (`IQueryDefinition`, `IQueryHandler`).
- **Result Pattern** : Gestion prédictible des retours et des erreurs métier sans dépendre des exceptions (`Result<T>`, `Error`).
- **Sécurité & RBAC** : Authentification par jetons **JWT Bearer** avec Refresh Token, hachage des mots de passe avec **PBKDF2 / Salt**, et contrôle d'accès basé sur les rôles (`Admin`, `Utilisateur`).
- **Protection IDOR (Insecure Direct Object Reference)** : Vérification stricte que chaque modification, suppression ou lecture de ressource est rattachée à l'utilisateur connecté via ses claims (`ClaimTypes.Sid`).

---

## 🚀 Fonctionnalités principales

### 1. 🔐 Authentification & Comptes (`/api/auth`)
- **Inscription** : Création d'un compte avec mot de passe sécurisé.
- **Connexion** : Authentification et délivrance d'un Access Token JWT et d'un Refresh Token.
- **Rafraîchissement** : Renouvellement du token JWT via la paire de tokens.

### 2. 🎯 Compétences Utilisateur (`/api/competence`)
- **Lecture** : Récupération des compétences de l'utilisateur connecté, ordonnées par position.
- **Ajout** : Ajout d'une compétence avec catégorie, niveau d'expertise, statut, visibilité, note et rattachement optionnel à un certificat.
- **Modification** : Mise à jour des informations de la compétence (sécurisée par utilisateur).
- **Suppression** : Suppression avec recalcul dynamique des positions restantes.
- **Repositionnement** : Réordonnancement par glisser-déposer / liste d'IDs (`PUT /api/competence/repositionner`).

### 3. 📜 Certificats Obtenus (`/api/certificat`)
- **CRUD complet** : Ajout, consultation, mise à jour et suppression de certificats.
- **Validation** : Attribut personnalisé `[DateAnnee(2000)]` garantissant la validité des dates d'obtention.
- **Association** : Possibilité de lier plusieurs compétences à un même certificat.

### 4. 🏷️ Catégories & Niveaux
- **Catégories (`/api/categorie`)** : Consultation pour les utilisateurs authentifiés ; Ajout et modification restreints aux **Administrateurs** (`[Authorize(Roles = nameof(Role.Admin))]`).
- **Niveaux (`/api/niveau`)** : Consultation des échelons de progression (Débutant, Intermédiaire, Avancé, Expert).

---

## 💻 Stack Technique

| Composant | Technologie |
|---|---|
| **Framework** | .NET 10 (C#) |
| **Accès aux données** | Entity Framework Core 10.0 |
| **Base de données** | Microsoft SQL Server (LocalDB ou Conteneur) |
| **Sécurité** | JWT Bearer, PBKDF2 (`Rfc2898DeriveBytes`), System.Security.Claims |
| **Documentation API** | Scalar API Reference (`Scalar.AspNetCore`), OpenAPI |
| **Conteneurisation** | Docker (Image multi-stage SDK / ASP.NET Runtime) |

---

## ⚙️ Prérequis

- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [SQL Server](https://www.microsoft.com/sql-server) ou instance `(localdb)\MSSQLLocalDB`
- [Docker](https://www.docker.com/) *(optionnel, pour l'exécution en conteneur)*

---

## 📦 Installation & Démarrage

### 1. Cloner le projet
```bash
git clone <url-du-depot>
cd GestionCompetences
```

### 2. Configuration
Vérifiez ou adaptez la chaîne de connexion dans `GestionCompetences.API/appsettings.json` :
```json
{
  "ConnectionStrings": {
    "database": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GestionCompetences;Integrated Security=True;Encrypt=True;Trust Server Certificate=False;"
  },
  "Jwt": {
    "Key": "VotreCleSecreteTresLonguePourLeJwtDauMoins32Caracteres!",
    "Issuer": "GestionCompetencesAPI",
    "Audience": "GestionCompetencesClient",
  }
}
```

### 3. Lancer l'API
```bash
dotnet run --project GestionCompetences.API
```
> ℹ️ Les migrations Entity Framework Core s'appliquent automatiquement au démarrage grâce à `Database.MigrateAsync()`.

### 4. Tester l'API
Rendez-vous sur la documentation interactive **Scalar** :
👉 `http://localhost:<port>/scalar/v1`

---

## 🐳 Déploiement avec Docker

Le projet intègre un `Dockerfile` multi-stage optimisé pour .NET 10 :

```bash
# Construire l'image
docker build -t gestion-competences-api .

# Exécuter le conteneur sur le port 8080
docker run -d -p 8080:8080 --name gestion-competences-api gestion-competences-api
```

---

## 📑 Endpoints de l'API

| Méthode | Route | Description | Autorisation |
|---|---|---|---|
| `POST` | `/api/auth` | Inscription d'un nouvel utilisateur | Public |
| `POST` | `/api/auth/login` | Connexion et obtention des tokens | Public |
| `POST` | `/api/auth/refresh` | Renouvellement de l'Access Token | Public |
| `GET` | `/api/competence` | Récupération des compétences de l'utilisateur | Authentifié |
| `POST` | `/api/competence` | Ajout d'une nouvelle compétence | Authentifié |
| `PUT` | `/api/competence` | Modification d'une compétence | Authentifié (Propriétaire) |
| `DELETE` | `/api/competence/{id}` | Suppression d'une compétence | Authentifié (Propriétaire) |
| `PUT` | `/api/competence/repositionner`| Réordonnancement des positions | Authentifié (Propriétaire) |
| `GET` | `/api/certificat` | Liste des certificats de l'utilisateur | Authentifié |
| `POST` | `/api/certificat` | Ajout d'un certificat | Authentifié |
| `PUT` | `/api/certificat` | Modification d'un certificat | Authentifié (Propriétaire) |
| `DELETE` | `/api/certificat/{id}` | Suppression d'un certificat | Authentifié (Propriétaire) |
| `GET` | `/api/categorie` | Liste des catégories de compétences | Authentifié |
| `POST` | `/api/categorie` | Ajout d'une catégorie | **Admin** |
| `PUT` | `/api/categorie` | Modification d'une catégorie | **Admin** |
| `GET` | `/api/niveau` | Liste des niveaux d'expertise | Authentifié |

