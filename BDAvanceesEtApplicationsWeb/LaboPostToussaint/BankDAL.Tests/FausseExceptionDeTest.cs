using System;
namespace BankDAL.Tests
{
    public class FausseExceptionDeTest : Exception
    {
        public FausseExceptionDeTest() : base("Exception levée pour les besoins des tests")
        {

        }
    }
}