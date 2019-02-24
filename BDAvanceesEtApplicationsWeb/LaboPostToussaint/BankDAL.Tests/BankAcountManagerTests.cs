using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace BankDAL.Tests
{
    [TestClass]
    public class BankAccountManagerTests
    {
        private const string COMPTE_EXISTANT_1="BE68539007547034";
        private const string COMPTE_EXISTANT_2="BE12345678912301";
         [TestInitialize]
        public void AvantChaqueTest()
        {
           BankAccountManager manager = new BankAccountManager();
           manager.SupprimerComptes();
           manager.CreerCompte(COMPTE_EXISTANT_1, 54.75);
           manager.CreerCompte(COMPTE_EXISTANT_2, 60.75);
        }

        [TestMethod]
        [ExpectedException(typeof(BankAccountNotFoundException))]
        public void LeveExceptionSiCompteEnBanqueOrigineInexistant()
        {
            BankAccountManager manager = new BankAccountManager();
            manager.TransfererArgent("CetIBANNexistePas", COMPTE_EXISTANT_1, 123);
        }

        [TestMethod]
        [ExpectedException(typeof(BankAccountNotFoundException))]
        public void LeveExceptionSiCompteEnBanqueDestinationInexistant()
        {
            BankAccountManager manager = new BankAccountManager();
            manager.TransfererArgent(COMPTE_EXISTANT_1, "CetIBANNexistePas", 123);
        }

        [TestMethod]
        public void TransfertFonctionne()
        {
            BankAccountManager manager = new BankAccountManager();
            manager.TransfererArgent(COMPTE_EXISTANT_1, COMPTE_EXISTANT_2, 12);
            Assert.AreEqual(42.75,manager.ObtenirSolde(COMPTE_EXISTANT_1));
            Assert.AreEqual(72.75,manager.ObtenirSolde(COMPTE_EXISTANT_2));
        }

        [TestMethod]
        public void ObtenirSoldeFonctionne()
        {
            BankAccountManager manager = new BankAccountManager();
            double solde = manager.ObtenirSolde(COMPTE_EXISTANT_1);
            //le solde de départ est défini dans le script de création de la base de données.
            Assert.AreEqual(54.75, solde);
        }

        [TestMethod]
        public void LaisseBaseDeDonneesDansEtatCoherentSiErreurDeTransfert()
        {
            BankAccountManager manager = new FailingBankAccountManager();
            try
            {
                manager.TransfererArgent(COMPTE_EXISTANT_1, "CetIBANNexistePas", 123);
            }
            catch (FausseExceptionDeTest ex){
                //on ne fait rien. Ce type d'exception est attendu puisque le BankAccountManager utilisé est spécifiquement conçu pour échouer lors du crédit d'un compte. Il réussira à faire le débit du compte émeteur du paiement, mais pas le crédit du compte de destination. 
             }
            double soldeApresOperation = manager.ObtenirSolde(COMPTE_EXISTANT_1);
            //le solde après opération doit être celui de départ, car l'opération de transfert n'a pas pu se produire
            //étant donné que le compte de destination n'existe pas.
            //solde de départ => voir TestInitialize
            Assert.AreEqual(54.75, soldeApresOperation);
        }
    }
}
