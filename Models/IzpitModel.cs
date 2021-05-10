using System;

namespace Listovki_TSP_Uni.Models
{
    public class IzpitModel
    {
        public int id { get; set; }
        public ListovkaModel listovka { get; set; }
        public int? listovkaId { get; set; }
        public KormuvaneModel kormuvane { get; set; }
        public int? kormuvaneId { get; set; }
        public IzpitModel()
        {

        }
    }
}
