using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
namespace DAL
{
    // L'idéal est de masquer l'utilisation d'EF derrière une façade. Objectif: pouvoir changer l'implémentation de la DAL si le besoin apparait. 
    public class DataAccess
    {
        private readonly Labo3Context context;
        public DataAccess(Labo3Context context)
        {
            this.context = context;
        }

        public async Task<Student> EtudiantParIdAsync(int id)
        {
            return await StudentQueryBase().FirstOrDefaultAsync(student=>student.Id==id);
        }

        // Les paramètres optionnels pageSize et pageIndex permettent d'implémenter le paging des résultats. 
        // Regardez la documentation sur les méthodes Skip et Take!
        public async Task<IEnumerable<Student>> ListerTousLesEtudiantsEtLeursInscriptionsAsync(int pageSize = 10, int pageIndex = 0)
        {
            // jointures sur deux niveaux, voir mode de chargement des entités (ressources dans support de cours)
            // Quelle méthode préférer? 
            //      a) appel à Include auquel on passe un string représentant le chemin vers le concept lié à charger?
            //      b) appel à Include auquel on passe une expression représentant le chemin vers le concept lié à charger?
            return await StudentQueryBase()
                            .Skip(pageIndex * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        }

        private Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Student, Course> StudentQueryBase()
        {
            return context.Student
                                        .Include(student => student.StudentCourse)
                                        .ThenInclude(studentCourse => studentCourse.Course);
        }

        public Student AjouterEtudiant(string fullName, DateTime birthDate)
        {

            return AjouterEtudiant(new Student()
            {
                FullName = fullName,
                Birthdate = birthDate
            });

            // regardez également aux versions asynchrones des méthodes!
        }

        public Student AjouterEtudiant(Student etudiant)
        {
            context.Student.Add(etudiant);
            context.SaveChanges();
            return etudiant;
            // regardez également aux versions asynchrones des méthodes!
        }

        public Course AjouterCours(string description)
        {
            var course = new Course()
            {
                Description = description
            };
            context.Course.Add(course);
            context.SaveChanges();
            return course;
        }

        public void AjouterInscriptionEtudiantAuCours_Variante1(long studentId, long courseId)
        {

            // Dans cette variante, ce sont les attributs représentant les Foreign Keys
            // qui sont utilisés pour spécifier les cours et étudiants à relier. 
            var inscription = new StudentCourse()
            {
                CourseId = courseId,
                StudentId = studentId
            };
            context.StudentCourse.Add(inscription);
            context.SaveChanges();
            // regardez également aux versions asynchrones des méthodes!
        }

        public async Task AjouterInscriptionEtudiantAuCours_Variante2Async(long studentId, long courseId)
        {

            /* Dans cette variante, les instances des étudiants et des cours
            sont explicitement recherchées. Rappel : pour rechercher une entité, il existe plusieurs méthodes. Les méthodes à utiliser varient selon les scenarii. Identifiez au moins deux scenarii de recherche et la méthode de recherche préférable dans chacun de ces scenarii. */
            Student student = await context.Student.FindAsync(studentId);
            if (student == null)
                throw new StudentNotFoundException(studentId);
            Course course = await context.Course.FindAsync(courseId);
            if (course == null)
                throw new CourseNotFoundException(courseId);
            var inscription = new StudentCourse()
            {
                Course = course,
                Student = student
            };
            context.StudentCourse.Add(inscription);
            await context.SaveChangesAsync();
        }

        public async Task SupprimerEtudiantAsync(long studentId)
        {

            // On réutilise la méthode existante de recherche de l'utilisateur sur base de son identifiant.
            // Pour quelle raison? Pourquoi ne pas simplement faire un context.Student.FindAsync(studentId)? 
            // => car pour supprimer les entités liées et exécuter les Cascade Delete (si configurés)
            //    il faut que les entités liées aient été chargées dans le contexte => il faut des Include!
            Student etudiantASupprimer = await EtudiantParIdAsync((int)studentId);
            if (etudiantASupprimer != null)
            {
                context.Student.Remove(etudiantASupprimer);
                // regardez également aux versions asynchrones des méthodes!
                // Attention: et si des inscriptions référencent encore l'étudiant à supprimer? Delete en cascade? Voir la configuration de votre mapping Code Objet/DB Relationnelle (classe XXXContext)
                context.SaveChanges();
            }
        }

        public Student ModifierEtudiant(Student etudiant)
        {
            // 2 cas de figures: 

            // 1: Si l'étudiant a été préalablement récupéré depuis le contexte (la même instance de DbContext que celle utilisée dans cette méthode)  via une des méthodes adéquates (ex: méthode Find, First...)
            // alors l'instance est déjà chargée dans le contexte et surveillée par le change tracker.
            // Le change tracker intégré au contexte va être averti que l'instance
            // a été modifiée et va lors du SaveChanges générer l'UPDATE statement
            // adéquat. Voir http://www.entityframeworktutorial.net/efcore/changetracker-in-ef-core.aspx 
            // il n'y a donc rien à faire!

            // 2: Si l'instance d'étudiant passée en paramètre n'est pas attachée au contexte, il faut l'y attacher et la marquer comme "modifiée" afin de forcer la génération de l'update statement lors du SaveChanges.

            // Ce mode de fonctionnement par entités détachées peut être utile pour des scenarii où la récupération de l'entitée et la sauvegarde des modifications ne se font pas en utilisant la même instance de DbContext. Typiquement, ce pourrait être le cas dans vos API's REST qui sont stateless. Une requête HTTP GET => obtenir les infos de l'étudiant. Plus tard, une requête HTTP PUT => mettre à jour l'étudiant. Chaque requête a sa propre instance de DbContext et il faut donc rattacher les entités lors du PUT. 

            if (context.Entry(etudiant).State == EntityState.Detached)
            {
                context.Attach(etudiant).State = EntityState.Modified;
            }

            // regardez également aux versions asynchrones des méthodes!
            context.SaveChanges();
            return etudiant;
        }
    }
}
