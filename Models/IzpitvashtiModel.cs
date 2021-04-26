using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Listovki_TSP_Uni.Models
{
    public class IzpitvashtiModel
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string egn { get; set; }

        public IzpitvashtiModel() { }
    }
}
