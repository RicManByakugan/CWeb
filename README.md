## Projet DotNet

1. [Aperçu](#aperçu)
2. [Fonctionnalités](#fonctionnalités)
    - [Front Office](#front-office)
    - [Back Office](#back-office)
    - [Services](#services)
    - [Management](#management)
3. [API Endpoints](#api-endpoints)
4. [Instructions pour les Patients](#instructions-pour-les-patients)
5. [Instructions pour le Personnel d'Accueil](#instructions-pour-le-personnel-daccueil)
6. [Instructions pour les Services](#instructions-pour-les-services)
7. [Instructions pour la Gestion](#instructions-pour-la-gestion)
8. [Guide d'Installation](#guide-dinstallation)

## Aperçu

CWEB est un système de gestion de clinique conçu pour améliorer l'efficacité des processus administratifs et cliniques. Il offre des interfaces distinctes pour les patients, le personnel d'accueil, les services médicaux et le management de la clinique.

## Fonctionnalités

### Front Office
  - SQL Server
  - ADO.net (Accès aux données)
  - MVC, REST API

### Back Office
  - SQL Server
  - Entity Framework
  - Razor Page

### Services

Les services médicaux disponibles comprennent :
1. Consultation de médecine générale
2. Maternité
3. Vaccination
4. Pédiatrie
5. Bloc opératoire
6. Dentaire

### Accueil Disponible
1. Accueil I
2. Accueil II
3. Accueil III

### Management

Les fonctionnalités de gestion permettent de superviser et de gérer le personnel, ainsi que de suivre les activités de l'accueil et des services.

## API Endpoints

- **POST**: `api/patient/add/` : Ajout Ticket pour un client
- **GET**: `api/patient/` : Liste des clients en attente en ce jour
- **GET**: `api/patient/{id}` : Détails d'un patient
- **GET**: `api/patient/accueil/{accueil}` : Liste des clients en attente dans un accueil spécifique en ce jour (ex: ACCUEIL 1)
- **GET**: `api/patient/service/{service}` : Liste des clients en attente dans un service spécifique en ce jour (ex: VACCINATION)

## Instructions pour les Patients

1. Prendre un ticket au départ.
2. Se diriger vers la file d'attente.
3. Attendre et voir quel accueil va réceptionner le patient.
4. Aller vers l'accueil, présenter les problèmes, payer les frais.
5. Recevoir la facture.
6. Se diriger vers le service correspondant après l'accueil (ex: Dentaire).
7. Attendre et voir si le service peut réceptionner.
8. Aller dans le service.

## Instructions pour le Personnel d'Accueil

1. Se connecter sur la plateforme.
2. Voir la file d'attente.
3. Mettre un ticket dans son box (annulable).
4. Questionner le patient et répondre à ses demandes.
5. Transférer le patient vers le service correspondant.
6. Compte journalier : Sortir la liste des réceptions du jour et transmettre à la direction financière.

### Fonctionnalités Supplémentaires

- Voir le chiffre d'affaires (CA) d'une journée.
- Voir les statistiques : services appelés, patients reçus d'une journée.
- Voir tout le CA effectué, nombre total.
- Voir toutes les statistiques : services appelés, patients reçus, nombre total.
- Voir le profil, voir le statut en ligne.
- Modifier le profil.
- Modifier le mot de passe.


## Instructions pour les Services

1. Se connecter sur la plateforme.
2. Voir la file d'attente.
3. Réceptionner le patient (annulable).
4. Laisser partir le patient après la réception.

### Fonctionnalités Supplémentaires

- Voir le profil, voir le statut en ligne.
- Modifier le profil.
- Modifier le mot de passe.
- Voir le chiffre d'affaires (CA) d'une journée.
- Voir les statistiques : patients reçus d'une journée.

## Instructions pour la Gestion

1. Voir la liste de tout le personnel, dans l'accueil, dans le service.
2. Voir les personnels en ligne.
3. Ajouter, modifier, supprimer, détail des personnels.
4. Réinitialiser le mot de passe d'un personnel en cas d'oubli.
5. Voir les activités de gestion du personnel.
6. Suivre les activités de tous les accueils journalier.
7. Suivre les activités de tous les services journalier.

### Fonctionnalités Supplémentaires

- Voir le profil, voir le statut en ligne.
- Modifier le profil.
- Modifier le mot de passe.

## Guide d'Installation

### Prérequis

1. **SDK .NET Core 8.0.204**
2. **SQL Server**
3. **Visual Studio 2019**

### Étapes d'Installation

1. **Cloner le Répertoire**

    ```bash
    git clone https://github.com/votre-repository/CWEB.git
    cd CWEB
    ```

2. **Installer le SDK .NET Core 8.0.204**

    Assurez-vous d'avoir installé le SDK .NET Core 8.0.204. Vous pouvez le télécharger depuis le [site officiel de .NET](https://dotnet.microsoft.com/download/dotnet/8.0).

3. **Configurer SQL Server**

    - Assurez-vous d'avoir SQL Server installé et en cours d'exécution.
    - Créez une base de données pour l'application.

4. **Configurer les Chaînes de Connexion**

    - Ouvrez le fichier `appsettings.json`.
    - Configurez la chaîne de connexion pour pointer vers votre instance SQL Server et votre base de données.

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
    }
    ```

5. **Restaurer les Packages NuGet**

    ```bash
    dotnet restore
    ```

6. **Appliquer les Migrations de la Base de Données**

    ```bash
    dotnet ef database update
    ```

7. **Exécuter l'Application**

    - Ouvrez le projet dans Visual Studio 2019.
    - Appuyez sur `F5` pour exécuter l'application.

8. **Accéder à l'Application**

    - Une fois l'application en cours d'exécution, vous pouvez y accéder via `http://localhost:5000` ou `https://localhost:5001`.

### Notes Supplémentaires

- Assurez-vous que vos ports SQL Server sont correctement configurés et que votre pare-feu autorise les connexions nécessaires.
- Si vous rencontrez des problèmes, vérifiez les journaux de l'application pour des détails spécifiques.

---