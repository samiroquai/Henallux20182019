#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

#define CODE_PAYS_BELGIQUE "54"
#define EAN_LONGUEUR 13

#define CALCUL_PRECONDITIONS_PAS_RESPECTEES 2
#define CALCUL_SUCCES 0
#define CODE_PAYS_LONGUEUR 2
#define CODE_FABRICANT_LONGUEUR 4
#define CODE_PRODUIT_LONGUEUR 6

// constantes relatives aux tests
#define TEST_ECHEC 1
#define TEST_SUCCES 0


int calculerEAN(char codePays[],char codeFabricant[], char codeProduit[], char *codeEAN){
    if(strlen(codePays)!=CODE_PAYS_LONGUEUR
    || strlen(codeFabricant)!=CODE_FABRICANT_LONGUEUR
    || strlen(codeProduit)!=CODE_PRODUIT_LONGUEUR)
        return CALCUL_PRECONDITIONS_PAS_RESPECTEES;
    char *debutEAN=codeEAN;
    strcpy(codeEAN,codePays);
    codeEAN+=CODE_PAYS_LONGUEUR;
    strcpy(codeEAN,codeFabricant);
    codeEAN+=CODE_FABRICANT_LONGUEUR;
    strcpy(codeEAN,codeProduit);
    codeEAN = debutEAN;
    int i=0;
    int checksum=0;
    while(*codeEAN!='\0'){
        int valeurCellule=*codeEAN-'0';
        checksum+=(i%2==0)?valeurCellule:valeurCellule*3;
        codeEAN++;
        i++;
    }
    int reste=checksum%10;
    reste=(reste==0)?0:10-reste;
    char resteChar='0'+reste;
    *codeEAN=resteChar;
    return CALCUL_SUCCES;
}

//fonction générique. Pourrait être définie dans une librairie. 
int AssertStringEquals(char *attendu, char *obtenu, char *errorMessage){
    if(strcmp(attendu,obtenu)!=0){
        printf(errorMessage, attendu, obtenu);
        return TEST_ECHEC;
    }
    return TEST_SUCCES;
}

int main(){
    // on déclare à longueur +1 pour inclure le caractère de fin de chaîne. 
    char obtenu[EAN_LONGUEUR+1]={0};
    char * attendu="5427731234569";
    int resultatCalcul=calculerEAN(CODE_PAYS_BELGIQUE,"2773","123456", obtenu);
    if(resultatCalcul!=CALCUL_SUCCES)
        return resultatCalcul;
    int res = AssertStringEquals(attendu, obtenu, "Le code EAN %s était attendu. Résultat obtenu: %s\n");
    return res;
}

