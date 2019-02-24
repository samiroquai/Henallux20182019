using System;

namespace DDDDemo.Model{
    public class InvalidOpeningPeriodException: BusinessException
    {
        public InvalidOpeningPeriodException()
        :base("L'heure d'ouverture doit être strictement inférieure à l'heure de fermeture")
        {
            
        }
    }
}