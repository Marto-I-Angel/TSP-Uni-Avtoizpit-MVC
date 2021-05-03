using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TSP_Uni_Listovki.Data;

namespace Listovki_TSP_Uni.Models
{
    public class ListovkaModel
    {
        [Key]
        public int id { get; set; }
        public DateTime timestamp { get; set; }
        public int tochki { get; set; }
        public virtual ApplicationUser user { get; set; }
        public string userId { get; set; }

        public ListovkaModel() { }
    }
}
