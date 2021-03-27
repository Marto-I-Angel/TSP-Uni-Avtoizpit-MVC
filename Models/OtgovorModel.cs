using System;
using System.ComponentModel.DataAnnotations;

namespace Listovki_TSP_Uni.Models
{
    public class OtgovorModel
    {
        [Key]
        public int id { get; set; }
        public string Content { get; set; }
        public string izobrajenie { get; set; }
        public bool veren { get; set; }
        public int VuprosID { get; set; }
        public virtual VuprosModel Vupros { get; set; }
        public OtgovorModel() { }
    }
}
