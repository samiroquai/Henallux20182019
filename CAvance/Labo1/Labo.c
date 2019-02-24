#include <stdio.h>
#include <stdlib.h>


#define NOMFICHIER "FiJeux.dat"
#define TITRELNMAX 100

typedef struct jeu Jeu;
struct jeu {
	char titre[TITRELNMAX];
	int nbAvis;
	double moyNotes;
};


void mettreEnMajuscule(char cars[]) {
	int i = 0;
	while (i < TITRELNMAX && cars[i] != '\n')
	{
		cars[i] = toupper(cars[i]);
		i++;
	}
}

void majFichier() {
	FILE *fichier;
	fopen_s(&fichier, NOMFICHIER, "rb+");
	if (fichier == NULL) {
		fopen_s(&fichier, NOMFICHIER, "w+");
	}
	Jeu jeuLu = { "",0,0 };
	char titreJeuRecherche[TITRELNMAX];
	int nouvelleNote = 0;
	char continuer = 'o';

	puts("Titre du jeu: (vide pour arr�ter)");
	gets_s(titreJeuRecherche, TITRELNMAX);
	while (strcmp(titreJeuRecherche, "") != 0) {
		mettreEnMajuscule(titreJeuRecherche);
		puts("Nouvelle note: ");
		scanf_s("%d", &nouvelleNote);
		fseek(fichier, 0, SEEK_SET);
		fread_s(&jeuLu, sizeof(Jeu), sizeof(Jeu), 1, fichier);
		while (!feof(fichier) && strcmp(jeuLu.titre, titreJeuRecherche) != 0) {
			fread_s(&jeuLu, sizeof(Jeu), sizeof(Jeu), 1, fichier);
		}
		if (feof(fichier)) {
			Jeu nouveauJeu = { .nbAvis = 1,.moyNotes = nouvelleNote };
			strcpy(nouveauJeu.titre, titreJeuRecherche);
			fwrite(&nouveauJeu, sizeof(Jeu), 1, fichier);
		}
		else {
			jeuLu.nbAvis++;
			jeuLu.moyNotes = (jeuLu.moyNotes*(jeuLu.nbAvis-1) + nouvelleNote) / jeuLu.nbAvis;
			long deplacement = (-1) * (long)sizeof(Jeu);
			int deplacementREalise = fseek(fichier, deplacement, SEEK_CUR);
			fwrite(&jeuLu, sizeof(Jeu), 1, fichier);
		}

		puts("Titre du jeu: (vide pour arr�ter)");
		getchar();
		gets_s(titreJeuRecherche, TITRELNMAX);
	}
	fclose(fichier);
}


void afficherFichier() {
	FILE *fichier;
	fopen_s(&fichier, NOMFICHIER, "rb");

	Jeu jeuLu = { "",0,0 };
	fread_s(&jeuLu, sizeof(Jeu), sizeof(Jeu), 1, fichier);
	while (!feof(fichier)) {
		printf("%s %d %f\n", jeuLu.titre, jeuLu.nbAvis, jeuLu.moyNotes);
		fread_s(&jeuLu, sizeof(Jeu), sizeof(Jeu), 1, fichier);
	}
	fclose(fichier);
}

void main() {


	majFichier();
	afficherFichier();
}