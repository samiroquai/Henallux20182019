using System;

namespace DDDDemo.Model{
    public class OverlappingOpeningPeriodsException: BusinessException
    {
        public OverlappingOpeningPeriodsException()
        :base("Deux périodes d'ouverture se chevauchent")
        {
            
        }
    }
}