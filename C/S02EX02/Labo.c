#include <stdio.h>
#include <string.h>
#include <stdbool.h>


bool estPalindrome(char entree[]){
    //printf("longueur du tableau: %d\n",strlen(entree));
    int i=0;
    int tailleChaine=strlen(entree);
    while(i<tailleChaine/2){
        if(entree[i]!=entree[tailleChaine-i-1])
            return false;
        i++;
    }
    return true;
}

void main(){
    if(!estPalindrome("kayak"))
        puts("Palindrome kayak non reconnu");
    if(!estPalindrome("elle"))
        puts("Palindrome elle non reconnu");
    if(estPalindrome("John"))
        puts("John reconnu comme palindrome");
    if(!estPalindrome("a"))
        puts("a reconnu comme palindrome");
}