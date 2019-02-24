using System;
using System.Collections.Generic;

namespace ComparaisonEFCoreADONet
{
    public partial class Badges
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
