

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DDDDemo.Model
{
    public class Shop
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<OpeningPeriod> OpeningPeriods { get; set; }
        public int OwnerId { get; set; }
        public byte[] RowVersion { get; set; }

        //Voir remarque sur constructeurs de OpeningPeriod
        private Shop()
        {
            OpeningPeriods = new List<OpeningPeriod>();
        }

        public Shop(string name, int ownerId)
        : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            OwnerId = ownerId;
        }

        public void AddOpeningPeriod(OpeningPeriod newPeriod)
        {
            #region ...
            if (OpeningPeriods.Any(existingPeriod =>
             existingPeriod.Day == newPeriod.Day &&
             (newPeriod.Closing >= existingPeriod.Opening && newPeriod.Closing <= existingPeriod.Closing) ||
             (newPeriod.Opening >= existingPeriod.Opening && newPeriod.Opening <= existingPeriod.Closing)
             || (newPeriod.Opening <= existingPeriod.Opening && newPeriod.Closing >= existingPeriod.Closing)))
                throw new OverlappingOpeningPeriodsException();
            #endregion




            this.OpeningPeriods.Add(newPeriod);
        }
    }
}