#include <stdio.h>
#include <string.h>

#define NOMFICHIER "FiJeux.dat"
#define TITRELNMAX 100
// EXIT CODES
#define ERREUR_FICHIER_OUVERTURE -1
#define SUCCES 0
#define TITREJEUSUPPRIME "********************"
typedef enum option Option;
enum option
{
    QUITTER = 'q',
    AJOUTER = 'a',
    MODIFIER = 'm',
    SUPPRIMER = 's',
    IMPRIMER = 'i'
};
typedef enum optionImpression OptionImpression;
enum optionImpression
{
    TOUT = 'a',
    JEU_PARTITRE = 't',
    // Dans l'énoncé il est question du prix, mais le prix n'est pas encodé en amont.
    //pour ne pas surcharger les étudiants je leur ai proposé de fonctionner en recherchant sur le nombre d'avis.
    JEU_PARNOMBREDAVIS = 'p'
};

typedef struct jeu Jeu;
struct jeu
{
    char titre[TITRELNMAX];
    int nbAvis;
    double moyNotes;
};
Option choixLu()
{
    puts("Que souhaitez-vous faire? Ajouter (a)? Supprimer (s)? Quitter (q)? Modifier (m)? Imprimer (i)");
    Option choix = (Option)getchar();
    getchar();
    return choix;
}

void ajouter()
{
    FILE *fichier = fopen(NOMFICHIER, "ab");
    if (fichier == NULL)
    {
        puts("Le fichier n'a pas pu être ouvert");
        return;
    }
    Jeu jeu = {"", 0, 0};
    puts("Nom du jeu: ");
    gets(jeu.titre);
    fwrite(&jeu, sizeof(Jeu), 1, fichier);
    fclose(fichier);
}

void modifierTitre(char *titreJeuAModifier, char *nouveauTitre)
{
    FILE *fichier = fopen(NOMFICHIER, "rb+");
    if (fichier == NULL)
    {
        puts("Erreur d'ouverture du fichier");
        return;
    }

    Jeu jeuLu = {"", 0, 0};
    fread(&jeuLu, sizeof(Jeu), 1, fichier);
    while (!feof(fichier) && strcmp(jeuLu.titre, titreJeuAModifier) != 0)
    {
        fread(&jeuLu, sizeof(Jeu), 1, fichier);
    }
    if (!feof(fichier))
    {
        strcpy(jeuLu.titre, nouveauTitre);
        fseek(fichier, -1 * sizeof(Jeu), SEEK_CUR);
        fwrite(&jeuLu, sizeof(Jeu), 1, fichier);
    }
    else
    {
        puts("Le jeu n'a pas été trouvé");
    }
    fclose(fichier);
}

void supprimer()
{
    puts("Quel jeu souhaitez-vous supprimer? Entrez son titre");
    char titreJeuASupprimer[TITRELNMAX];
    gets(titreJeuASupprimer);
    modifierTitre(titreJeuASupprimer, TITREJEUSUPPRIME);
}

void modifier()
{
    puts("Quel jeu souhaitez-vous modifier? Entrez son titre actuel");
    char titreJeuASupprimer[TITRELNMAX];
    gets(titreJeuASupprimer);
    puts("Entrez son nouveau titre");
    char nouveauTitre[TITRELNMAX];
    gets(nouveauTitre);
    modifierTitre(titreJeuASupprimer, nouveauTitre);
}

void afficherJeu(Jeu jeu)
{
    printf("%s %d %f\n", jeu.titre, jeu.nbAvis, jeu.moyNotes);
}

void imprimerTousLesJeux()
{
    FILE *fichier = fopen(NOMFICHIER, "rb");
    if (fichier == NULL)
    {
        puts("Erreur d'ouverture du fichier");
        return;
    }
    Jeu jeuLu = {"", 0, 0};
    fread(&jeuLu, sizeof(Jeu), 1, fichier);
    while (!feof(fichier))
    {
        afficherJeu(jeuLu);
        fread(&jeuLu, sizeof(Jeu), 1, fichier);
    }
    fclose(fichier);
}

void rechercherJeuParTitre()
{
    puts("Quel jeu souhaitez-vous afficher?");
    char titreJeuARechercher[TITRELNMAX];
    gets(titreJeuARechercher);
    FILE *fichier = fopen(NOMFICHIER, "rb+");
    if (fichier == NULL)
    {
        puts("Erreur d'ouverture du fichier");
        return;
    }

    Jeu jeuLu = {"", 0, 0};
    fread(&jeuLu, sizeof(Jeu), 1, fichier);
    while (!feof(fichier) && strcmp(jeuLu.titre, titreJeuARechercher) != 0)
    {
        fread(&jeuLu, sizeof(Jeu), 1, fichier);
    }
    if (feof(fichier))
    {
        puts("Le jeu n'a pas été trouvé");
        return;
    }
    afficherJeu(jeuLu);
    fclose(fichier);
}

void rechercherJeuxParNombreDavis()
{
    //TODO: implémenter cette méthode
    puts("Combien d'avis doivent-avoir les jeux que souhaitez-vous afficher?");
    int nbrAvisARechercher;
    scanf("%d", &nbrAvisARechercher);
    getchar();
    FILE *fichier = fopen(NOMFICHIER, "rb+");
    if (fichier == NULL)
    {
        puts("Erreur d'ouverture du fichier");
        return;
    }

    Jeu jeuLu = {"", 0, 0};
    fread(&jeuLu, sizeof(Jeu), 1, fichier);
    while (!feof(fichier))
    {
        if (jeuLu.nbAvis == nbrAvisARechercher)
            afficherJeu(jeuLu);
        fread(&jeuLu, sizeof(Jeu), 1, fichier);
    }
    fclose(fichier);
}

void imprimer()
{
    puts("Que souhaitez-vous imprimer? Tous les jeux (a)? Un jeu par son titre (t)? Tous les jeux ayant reçu un certain nombre d'avis (p)?");
    OptionImpression choix = (OptionImpression)getchar();
    getchar();
    switch (choix)
    {
    case TOUT:
        imprimerTousLesJeux();
        break;
    case JEU_PARTITRE:
        rechercherJeuParTitre();
        break;
    case JEU_PARNOMBREDAVIS:
        rechercherJeuxParNombreDavis();
        break;
    default:
        puts("Option d'impression non supportée");
    }
}
void main()
{
    Option choix = choixLu();
    while (choix != QUITTER)
    {
        switch (choix)
        {
        case AJOUTER:
            ajouter();
            break;
        case SUPPRIMER:
            supprimer();
            break;
        case MODIFIER:
            modifier();
            break;
        case IMPRIMER:
            imprimer();
            break;
        default:
            puts("Inconnu");
        }
        choix = choixLu();
    }
}