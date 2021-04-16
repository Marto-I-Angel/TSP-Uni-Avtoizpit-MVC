using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Listovki_TSP_Uni.Models
{
    public class VuprosiZaListovka
    {
        [Key]
        public int id { get; set; }
        public int? ListovkaID { get; set; }
        public virtual ListovkaModel Listovka { get; set; }
        public int? VuprosId { get; set; }
        public virtual VuprosModel Vupros { get; set; }

        public VuprosiZaListovka() { }
    }
}
