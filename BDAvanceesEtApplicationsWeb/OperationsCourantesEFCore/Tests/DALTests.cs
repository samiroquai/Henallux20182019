using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
namespace Tests
{
    [TestClass]
    public class DALTests
    {
        private DAL.DataAccess dataAccess;
        [TestInitialize]
        public void Setup()
        {
            // Cette méthode est exécutée avant de jouer chaque test de la classe.
            // elle permet de repartir d'un contexte vierge. 
            // voir https://www.meziantou.net/2018/02/12/mstest-v2-test-lifecycle-attributes 
            dataAccess = new DataAccess(GetContext());
        }

        protected virtual Labo3Context GetContext()
        {
            DbContextOptionsBuilder<Labo3Context> builder = new DbContextOptionsBuilder<Labo3Context>();
            string connectionString = null;
            if (connectionString == null)
                throw new NotSupportedException("Veuillez spécifier votre connection string :). L'idéal étant de la récupérer depuis la configuration (voir labo et autre projet exemple à disposition)");
            builder.UseSqlServer(connectionString);
            return new Labo3Context(builder.Options);
        }

        [TestMethod]
        public async Task ListingEtudiantsAsync()
        {
            var students = await dataAccess.ListerTousLesEtudiantsEtLeursInscriptionsAsync();
            Assert.IsTrue(students.Count() > 0);
            Assert.IsTrue(students.Any(s => s.StudentCourse.Any()));
            Assert.IsTrue(students.Any(s => s.StudentCourse.Any() && s.StudentCourse.First().Course != null));
        }


        [TestMethod]
        public async Task PagingAsync()
        {
            // Cet exemple veut illustrer le paging qui vous sera demandé dans la conception de votre API.

            //ASSERT: d'abord, nous allons créer une centaine d'étudiants pour s'assurer que la DB n'est pas vide
            for (int i = 0; i < 100; i++)
            {
                dataAccess.AjouterEtudiant(Guid.NewGuid().ToString("N"), DateTime.Now.AddYears(-1));
            }

            // ACT: on liste les étudiants en demandant une tranche de ces derniers. 
            IEnumerable<Student> students = await dataAccess.ListerTousLesEtudiantsEtLeursInscriptionsAsync(10, 0);
            Assert.AreEqual(10, students.Count());
            IEnumerable<Student> otherStudents = await dataAccess.ListerTousLesEtudiantsEtLeursInscriptionsAsync(10, 1);
            Assert.AreEqual(10, otherStudents.Count());
        }

        [TestMethod]
        public void AjouterEtudiant()
        {
            // UNIT TEST  = AAA (Triple A): ARRANGE, ACT, ASSERT.

            // ARRANGE => on met en place le contexte de tests
            string nomUnique = System.Guid.NewGuid().ToString("N");
            DateTime dateNaissance = new DateTime(2018, 1, 1).Date;

            // ACT => on agit, on utilise la méthode dont on souhaite vérifier le fonctionnement
            dataAccess.AjouterEtudiant(nomUnique, dateNaissance);

            // ASSERT => on vérifie. 
            bool estEtudiantRetrouveApresCreation = EstEtudiantRetrouve(nomUnique, dateNaissance);
            Assert.IsTrue(estEtudiantRetrouveApresCreation);
        }

        [TestMethod]
        public void AjouterInscriptionEtudiantAUnCours_Variante1()
        {
            // ARRANGE => on crée un étudiant et un cours que nous allons lier. C'est l'action de liaison que nous testons.
            // Créer l'étudiant et le  cours au runtime nous permet d'être certain que nous avons de la "matière" sur laquelle
            // baser les tests. 
            string nomEtudiantUnique = System.Guid.NewGuid().ToString("N");
            DateTime dateNaissance = new DateTime(2018, 1, 1).Date;
            Student etudiant = dataAccess.AjouterEtudiant(nomEtudiantUnique, dateNaissance);

            string nomCoursUnique = System.Guid.NewGuid().ToString("N");
            Course cours = dataAccess.AjouterCours(nomCoursUnique);


            // ACT
            dataAccess.AjouterInscriptionEtudiantAuCours_Variante1(etudiant.Id, cours.Id);

            // ASSERT
            bool estInscriptionRetrouvee = EstInscriptionRetrouvee(cours.Id, etudiant.Id);
            Assert.IsTrue(estInscriptionRetrouvee);
        }

        [TestMethod]
        [ExpectedException(typeof(Microsoft.EntityFrameworkCore.DbUpdateException))]
        public void AjouterInscriptionEtudiantAUnCours_Variante1_CoursEtEtudiantsInconnus()
        {
            // ARRANGE
            long idEtudiantInexistant = -1;
            long idCoursInexistant = -1;

            // ACT
            dataAccess.AjouterInscriptionEtudiantAuCours_Variante1(idEtudiantInexistant, idCoursInexistant);

            // ASSERT implicite. Voir remarque sur ExpectedException dans les autres tests de cette classe.

            // Dans la variante 1, la vérification des contraintes se fait au niveau SGBD. Pour 
            // savoir ce qui a posé problème, il faut examiner l'erreur retournée par le SGBD. 
        }

        [TestMethod]
        public async Task AjouterInscriptionEtudiantAUnCours_Variante2Async()
        {
            // ARRANGE => on crée un étudiant et un cours que nous allons lier. C'est l'action de liaison que nous testons.
            // Créer l'étudiant et le  cours au runtime nous permet d'être certain que nous avons de la "matière" sur laquelle
            // baser les tests. 
            string nomEtudiantUnique = System.Guid.NewGuid().ToString("N");
            DateTime dateNaissance = new DateTime(2018, 1, 1).Date;
            Student etudiant = dataAccess.AjouterEtudiant(nomEtudiantUnique, dateNaissance);

            string nomCoursUnique = System.Guid.NewGuid().ToString("N");
            Course cours = dataAccess.AjouterCours(nomCoursUnique);


            // ACT
            await dataAccess.AjouterInscriptionEtudiantAuCours_Variante2Async(etudiant.Id, cours.Id);

            // ASSERT
            bool estInscriptionRetrouvee = EstInscriptionRetrouvee(cours.Id, etudiant.Id);
            Assert.IsTrue(estInscriptionRetrouvee);
        }

        [TestMethod]
        [ExpectedException(typeof(CourseNotFoundException))]
        public async Task AjouterInscriptionEtudiantAUnCours_Variante2_CoursInconnuAsync()
        {
            // ARRANGE => on crée un étudiant et un cours que nous allons lier. C'est l'action de liaison que nous testons.
            // Créer l'étudiant et le  cours au runtime nous permet d'être certain que nous avons de la "matière" sur laquelle
            // baser les tests. 
            string nomEtudiantUnique = System.Guid.NewGuid().ToString("N");
            DateTime dateNaissance = new DateTime(2018, 1, 1).Date;
            Student etudiant = dataAccess.AjouterEtudiant(nomEtudiantUnique, dateNaissance);

            long idCoursInconnu = -1;

            // ACT
            await dataAccess.AjouterInscriptionEtudiantAuCours_Variante2Async(etudiant.Id, idCoursInconnu);

            // ASSERT implicite => c'est le ExpectedException. Le test est validé si une exception du type attendu est levée. 
            // Dans la variante 2, la vérification des contraintes se fait au niveau applicatif. 
            // L'erreur peut être déterminée facilement sans devoir parser le retour de l'erreur SQL renvoyée par le SGBD puisqu'elle est détectée avant d'aller vers celui-ci avec la requête d'insertion. 

        }

        [TestMethod]
        [ExpectedException(typeof(StudentNotFoundException))]
        public async Task AjouterInscriptionEtudiantAUnCours_Variante2_EtudiantInconnuAsync()
        {
            // ARRANGE => on crée un étudiant et un cours que nous allons lier. C'est l'action de liaison que nous testons.
            // Créer l'étudiant et le  cours au runtime nous permet d'être certain que nous avons de la "matière" sur laquelle
            // baser les tests. 
            long idEtudiantInconnu = -1;
            string nomCoursUnique = System.Guid.NewGuid().ToString("N");
            Course cours = dataAccess.AjouterCours(nomCoursUnique);

            // ACT
            await dataAccess.AjouterInscriptionEtudiantAuCours_Variante2Async(idEtudiantInconnu, cours.Id);

            // ASSERT implicite (voir ExpectedException)

        }

        [TestMethod]
        public async Task SupprimerEtudiant()
        {
            // ARRANGE => on crée un étudiant et un cours que nous allons lier. C'est l'action de liaison que nous testons.
            string nomEtudiantUnique = System.Guid.NewGuid().ToString("N");
            DateTime dateNaissance = new DateTime(2018, 1, 1).Date;
            Student etudiant = dataAccess.AjouterEtudiant(nomEtudiantUnique, dateNaissance);

            // ACT
            dataAccess.SupprimerEtudiant(etudiant.Id);

            //ASSERT
            bool estEtudiantToujoursPresent = EstEtudiantRetrouve(nomEtudiantUnique, dateNaissance);
            Assert.IsFalse(estEtudiantToujoursPresent);

        }

        [TestMethod]
        public async Task MiseAJourEtudiant_EtudiantAttacheAuContexte()
        {
            // ARRANGE
            string nomEtudiantUnique = System.Guid.NewGuid().ToString("N");
            DateTime dateNaissance = new DateTime(2018, 1, 1).Date;
            Student etudiant = dataAccess.AjouterEtudiant(nomEtudiantUnique, dateNaissance);

            // ACT
            etudiant.Birthdate = new DateTime(2016, 1, 1);
            etudiant.FullName = "John Doe";
            dataAccess.ModifierEtudiant(etudiant);

            //ASSERT
            bool estEtudiantModifie = EstEtudiantRetrouve("John Doe", new DateTime(2016, 1, 1), etudiant.Id);
            Assert.IsTrue(estEtudiantModifie);
        }

        [TestMethod]
        public async Task MiseAJourEtudiant_EtudiantDetacheDuContexte()
        {
            // ARRANGE => un peu particulier. Il faut créer un étudiant dans un autre DbContext, afin qu'il soit détaché du contexte qui sera utilisé pour le test (partie ACT)
            Student etudiantHorsContexte = CreerEtudiantDansContexteSepare(System.Guid.NewGuid().ToString("N"), new DateTime(2018, 1, 1));

            // ACT
            etudiantHorsContexte.Birthdate = new DateTime(2016, 1, 1);
            etudiantHorsContexte.FullName = "John Doe";
            dataAccess.ModifierEtudiant(etudiantHorsContexte);

            //ASSERT
            bool estEtudiantModifie = EstEtudiantRetrouve("John Doe", new DateTime(2016, 1, 1), etudiantHorsContexte.Id);
            Assert.IsTrue(estEtudiantModifie);
        }




        #region méthodes utilitaires aidant aux tests
        private Student CreerEtudiantDansContexteSepare(string nomEtudiant, DateTime dateNaissance)
        {
            var tempDataAccess = new DataAccess(GetContext());
            Student etudiant = tempDataAccess.AjouterEtudiant(nomEtudiant, dateNaissance);
            return etudiant;
        }


        protected bool EstEtudiantRetrouve(string nomUnique, DateTime dateNaissance, long? id = null)
        {
            // on utilise un nouveau contexte => de la sorte on est certain de ne pas avoir de cache 
            // qui fausserait les tests.
            using (var context = GetContext())
            {
                return context.Student.Any(student => student.FullName.Equals(nomUnique) && student.Birthdate.HasValue && student.Birthdate.Value == dateNaissance && (!id.HasValue || student.Id == id.Value));
            }
        }

        protected bool EstInscriptionRetrouvee(long coursId, long etudiantId)
        {
            using (var context = GetContext())
            {
                return context.StudentCourse.Any(c => c.CourseId == coursId && c.StudentId == etudiantId);
            }
        }
    }
    #endregion
}
