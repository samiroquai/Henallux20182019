#include <stdio.h>
#include <string.h>
#include <stdbool.h>


//V1
bool estPalindromeTableau(char entree[]){
    int i=0;
    int tailleChaine=strlen(entree);
    while(i<tailleChaine/2 && entree[i]==entree[tailleChaine-i-1]){
        i++;
    }
    return i>=tailleChaine/2;
}

//V2
bool estPalindromePointeur(char* mot, int taille) {
	char * pDebut = mot;
	char * pFin = mot + (taille - 1); // se place sur le dernier carcatère
	while ((pFin > pDebut) && (*pDebut == *pFin)) {
		pDebut++;
		pFin--;
	}
	return pFin <= pDebut;
}

bool estPalindromeRecursif(char mot[], int debut, int fin){
    if(debut>fin)
        return true;
    return mot[debut]==mot[fin] && estPalindromeRecursif(mot,debut+1,fin-1);
}

bool estPalindromeRecursifTerminal(char mot[], int debut, int fin){
    if(debut>=fin)
        return true;
    if(mot[debut]==mot[fin])
        return estPalindromeRecursifTerminal(mot,debut+1,fin-1);
    else
        return false;
}


int main(){
    if(!estPalindromeTableau("kayak"))
        puts("Palindrome kayak non reconnu (tableau)");
    if(!estPalindromeTableau("elle"))
        puts("Palindrome elle non reconnu (tableau)");
    if(estPalindromeTableau("John"))
        puts("John reconnu comme palindrome (tableau)");
    if(!estPalindromeTableau("a"))
        puts("a pas reconnu comme palindrome (tableau)");

    if(!estPalindromePointeur("kayak",5))
        puts("Palindrome kayak non reconnu (pointeur)");
    if(!estPalindromePointeur("elle",4))
        puts("Palindrome elle non reconnu (pointeur)");
    if(estPalindromePointeur("John",4))
        puts("John reconnu comme palindrome (pointeur)");
    if(!estPalindromePointeur("a",1))
        puts("a pas reconnu comme palindrome (pointeur)");

    if(!estPalindromeRecursif("kayak",0,4))
        puts("Palindrome kayak non reconnu (récursif)");
    if(!estPalindromeRecursif("elle",0,3))
        puts("Palindrome elle non reconnu (récursif)");
    if(estPalindromeRecursif("John",0,3))
        puts("John reconnu comme palindrome (récursif)");
    if(!estPalindromeRecursif("a",0,0))
        puts("a pas reconnu comme palindrome (récursif)");

    if(!estPalindromeRecursifTerminal("kayak",0,4))
        puts("Palindrome kayak non reconnu (récursif terminal)");
    if(!estPalindromeRecursifTerminal("elle",0,3))
        puts("Palindrome elle non reconnu (récursif terminal)");
    if(estPalindromeRecursifTerminal("John",0,3))
        puts("John reconnu comme palindrome (récursif terminal)");
    if(!estPalindromeRecursifTerminal("a",0,0))
        puts("a pas reconnu comme palindrome (récursif terminal)");

    return 0;
}