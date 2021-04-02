using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listovki_TSP_Uni.Models
{
    public class VuprosModel
    {

        public int id { get; set; }
        public string uslovie { get; set; }
        public string img { get; set; }
        public int tochki { get; set; }
        public int? ListovkaID { get; set; }
        public virtual ListovkaModel Listovka { get; set; }
        public virtual ICollection<OtgovorModel> Otgovori { get; set; }
        public VuprosModel()
        {

        }
    }

}

