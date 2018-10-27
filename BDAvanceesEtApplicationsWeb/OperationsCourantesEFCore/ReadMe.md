# Opérations courantes en EF

Ce projet de démo tente d'illustrer la manière de réaliser certaines opérations courantes en EF (en l'occurence le scope CRUD). Il s'appuie sur le domaine que vous avez utilisé pour le labo DAL dans le cadre du cours. (notion de **Student**, **Course**, **StudentCourse**)

Lisez bien les commentaires répartis dans le code. 

Privilégiez les versions asynchrones des méthodes lorsque celles-ci sont disponibles.

Si votre code ne compile pas suite à des erreurs de types "méthodes inconnues", vérifiez bien les namespaces. En effet, beaucoup de méthodes utilisées en EF Core ou en Linq sont des méthodes d'extension.

## Exécution du code

Cette solution est constituée de plusieurs projets: 
* Model: le modèle du domaine d'application
* DAL : la couche de persistence de ce modèle
* Tests: de type [mstest](https://github.com/dotnet/docs/blob/master/docs/core/testing/unit-testing-with-mstest.md). Il s'agit de tests unitaires. 

Le projet de test illustre comment appeler la DAL (différents cas de tests). 

N'essayez pas de lancer ces tests, ils ne fonctionneront probablement pas chez vous. En effet

* il risque d'exister des différences au niveau du nommage de vos tables/attributs, rendant le mapping caduque 
* vous avez peut-être utilisé des schémas de DB différents.

Contentez-vous d'une lecture attentive du code et des commentaires qui accompagnent ce dernier

## Remarques

Le code de la DAL mélange méthodes synchrones et asynchrones pour illustrer que les deux approches sont possibles. Dans la mesure du possible, privilégiez les versions asynchrones des méthodes pour améliorer les performances.

## Aller plus loin

Essayez vous-même de créer du comportement similaire à celui de ce projet exemple. Lorsqu'on voit les choses "toutes faites", elles semblent évidentes. Lorsqu'il faut les créer soi-même, c'est autre chose et ce n'est qu'à ce moment-là que les déclics se feront. 

Essayez par exemple de créer un nouveau modèle sur base de la DB StackOverflow2 disponible sur le serveur de l'IESN et tentez d'exploiter ce dernier. 

