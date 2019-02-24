# PostPhotoWithOtherInfo

Ce projet de démo présente une manière de réaliser un POST en multipart/form-data depuis Angular en utilisant le HttpClient. Le Post réalise un upload de fichier. En plus du fichier, la requête http contient des méta-données relatives au fichier (en l'occurence, un "concept" auquel sera attaché le fichier uploadé).

Avant de l'exécuter, restaurer les dépendances npm via `npm install`

Il envoie les photos vers une api écoutant sur http://localhost:5000/api/Photos. Un exemple d'un endpoint compatible est disponible dans le projet [ImageStorageDemoNetCore](../../BDAvanceesEtApplicationsWeb/ImageStorageDemoNetCore/Readme.md )

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 6.2.2.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.
