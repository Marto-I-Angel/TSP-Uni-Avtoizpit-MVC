using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Listovki_TSP_Uni.Models
{
    public class InstruktoriModel
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string egn { get; set; }
        public string phone { get; set; }
        public string organisation { get; set; }

        public InstruktoriModel() { }
    }
}
