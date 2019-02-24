
using System.Data.SqlClient;
using System;
namespace BankDAL.Tests{

    public class FailingBankAccountManager : BankAccountManager
    {
        protected override void CrediterDe(int montantAAjouter, string iban, SqlConnection cn)
        {
           throw new FausseExceptionDeTest();
        } 
    }
}