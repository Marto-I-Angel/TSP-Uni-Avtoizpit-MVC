using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Listovki_TSP_Uni.Models
{
    public class ListovkaModel
    {
        [Key]
        public int id { get; set; }
        public DateTime timestamp { get; set; }
        public int tochki { get; set; }
        public virtual ICollection<VuprosModel> Vuprosi { get; set; }
        public string userName { get; internal set; }

        public ListovkaModel() { }
    }
}
