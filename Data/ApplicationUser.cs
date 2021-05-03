using Listovki_TSP_Uni.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TSP_Uni_Listovki.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<ListovkaModel> listovki { get; set; }
        public ApplicationUser()
        {

        }
    }
}