#include <stdio.h>
#include <string.h>
#include <stdbool.h>

int nbrOccurencesMotPhrase(char *mot, char *phrase){
    int compteur = 0;
    char *motCopie = mot;
    while(*phrase!='.'){
        if(*motCopie=='\0'){
            compteur++;
            motCopie=mot;
        }else if(*motCopie==*phrase){
            motCopie++;
        }else{
            motCopie=mot;
        }
        phrase++;
    }
    //cas de l'occurence en fin de phrase.
    if(*motCopie=='\0')
        compteur++;
    return compteur;
}

void main(){
    //n fois dans la phrase, dont première occurence en début de phase
    if(nbrOccurencesMotPhrase("le","le bonheur est dans le pré.")!=2)
        puts("le n'a pas été retrouvé deux fois dans le bonheur est dans le pré.");
    //occurence en fin de phrase
    if(nbrOccurencesMotPhrase("jamais","N'oubliez jamais.")!=1)
        puts("jamais n'a pas été retrouvé une fois dans N'oubliez jamais.");
    //aucune occurence
    if(nbrOccurencesMotPhrase("azeazeaze","Ceci est un test.")!=0)
        puts("azeazeaze a été retrouvé dans Ceci est un test.");
}