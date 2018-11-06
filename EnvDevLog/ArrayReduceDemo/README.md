# Reduce - exemple

## Introduction

Cette application Angular 6 illustre une utilisation de la fonction reduce. 

Le cas d'utilisation est le suivant: un appel à une API HTTP distante [OpenWeatherMap](https://openweathermap.org/forecast5) est réalisé. Cet appel retourne des prévisions météo pour 5 jours avec une prévision par tranche de trois heures. Or l'application ne doit afficher qu'une seule prévision par jour. 

Le [reduce](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reduce) est donc utilisé ici pour "réduire" la liste originale afin de ne conserver qu'une seule entrée par jour. 

Deux approches sont comparées: 
* en utilisant une fonction de réduction se basant sur un tableau et la méthode [find](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/find)
* en utilisant une fonction de réduction se basant sur les Associative Arrays 

Chaque approche est exécutée un certain nombre de fois et le temps d'exécution moyen est affiché sous la liste réduite. Vous pourrez constater que les deux fonctions arrivent au même résultat, mais sont très différentes en termes de performances.

## Exécution

Pour lancer l'application, installez d'abord les dépendances (ouvrez une invite de commandes dans le répertoire du projet).

```Bash
npm install
```

Lancez ensuite le bundling et le serveur web de développement à l'aide de la commande suivante (requiert un environnement Angular à disposition).

```
ng serve
```

Ouvrez ensuite un navigateur sur l'url http://localhost:4200.

Afin d'améliorer les performances, lancez l'application Angular en mode "production".

```Bash
ng serve --prod
```

