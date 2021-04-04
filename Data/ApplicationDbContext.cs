using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Listovki_TSP_Uni.Models;

namespace TSP_Uni_Listovki.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Listovki_TSP_Uni.Models.OtgovorModel> OtgovorModel { get; set; }
        public DbSet<Listovki_TSP_Uni.Models.VuprosModel> VuprosModel { get; set; }
        public DbSet<Listovki_TSP_Uni.Models.ListovkaModel> ListovkaModel { get; set; }
        public DbSet<Listovki_TSP_Uni.Models.VuprosiZaListovka> VuprosiZaListovka { get; set; }
    }
}
