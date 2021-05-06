using System;

namespace Listovki_TSP_Uni.Models
{
    public class IzpitModel
    {
        public int id { get; set; }
        public ListovkaModel listovka { get; set; }
        public KormuvaneModel kurmuvane { get; set; }
        public IzpitModel()
        {

        }
    }
}
