using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Listovki_TSP_Uni.Models;
using Microsoft.AspNetCore.Identity;

namespace TSP_Uni_Listovki.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>();
            //builder.Ignore<IdentityRole>();
            //builder.Ignore<IdentityUserClaim<string>>();
            //builder.Ignore<IdentityUserRole<string>>();
            //builder.Ignore<IdentityRoleClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserToken<string>>();
        }
        public DbSet<Listovki_TSP_Uni.Models.OtgovorModel> OtgovorModel { get; set; }
        public DbSet<Listovki_TSP_Uni.Models.VuprosModel> VuprosModel { get; set; }
        public DbSet<Listovki_TSP_Uni.Models.ListovkaModel> ListovkaModel { get; set; }
        public DbSet<Listovki_TSP_Uni.Models.VuprosiZaListovka> VuprosiZaListovka { get; set; }
    }
}
