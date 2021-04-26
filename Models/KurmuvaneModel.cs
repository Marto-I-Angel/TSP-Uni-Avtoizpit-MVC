using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Listovki_TSP_Uni.Models
{
    public class KurmuvaneModel
    {
        [Key]
        public int id { get; set; }
        public int karani_chasove { get; set; }
        public int tochki { get; set; }
        public virtual InstruktoriModel instruktor { get; set; }
        public virtual IzpitvashtiModel izpitvasht { get; set; }

        public KurmuvaneModel() { }
    }
}
