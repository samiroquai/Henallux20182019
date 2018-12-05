

using System;
using System.ComponentModel.DataAnnotations;

namespace DDDDemo.Model{
    public class OpeningPeriod
    {
        public virtual int Id{get;set;}
        public virtual TimeSpan Opening { get; set; }
        public virtual TimeSpan Closing { get; set; }
        public virtual DayOfWeek Day{get;set;}
        public byte[] RowVersion { get; set; }
        //constructeur par défaut nécessaire pour EF Core.
        // On le met en private afin d'empêcher instanciation par d'autres canaux. 
        // Les utilisateurs devront passer par le second constructeur. De la sorte on s'assure
        // que toute période d'ouverture créée l'est de manière complète (tout est renseigné) et cohérente
        // (pas de fermeture avant/en même temps que l'ouverture).
        private OpeningPeriod(){

        }
        public OpeningPeriod(TimeSpan opening, TimeSpan closing, DayOfWeek day)
        {
            if(opening>=closing)
                throw new InvalidOpeningPeriodException();
            Opening=opening;
            Closing=closing;
            Day=day;
        }
    }
}